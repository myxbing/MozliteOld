using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Mozlite.Utils;

namespace Mozlite.Extensions.Tasks
{
    /// <summary>
    /// 百度百科源代码。
    /// </summary>
    public class BaiduBaikeSource : IEnumerable<KeyValuePair<string, HtmlSource>>
    {
        private readonly string _url;
        private const string UrlFormat = "http://baike.baidu.com/item/{0}";
        private const string ReferenceUrl = "http://baike.baidu.com/";
        private string _source;
        /// <summary>
        /// 初始化类<see cref="BaiduBaikeSource"/>。
        /// </summary>
        /// <param name="urlOrKeyword">当前百度百科的关键词或地址。</param>
        public BaiduBaikeSource(string urlOrKeyword)
        {
            if (!urlOrKeyword.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                _url = string.Format(UrlFormat, urlOrKeyword);
            else
                _url = urlOrKeyword;
        }

        /// <summary>
        /// 获取HTML代码，并返回是否获取成功。
        /// </summary>
        /// <returns>返回获取结果。</returns>
        public async Task<bool> EnsureHtmlAsync()
        {
            var source = await HttpHelper.GetHtmlAsync(_url, ReferenceUrl);
            if (!string.IsNullOrWhiteSpace(source))
            {
                source = _supRegex.Replace(source, string.Empty);
                GetBasic(source);
                _source = source;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 元数据。
        /// </summary>
        public string GetMeta(string name)
        {
            if (_metas == null)
                return null;
            string value;
            _metas.TryGetValue(name, out value);
            return value;
        }

        private const string BasicStart = "<div class=\"basic-info cmn-clearfix\">";
        private const string BasicNameStart = "basicInfo-item name\">";
        private const string BasicValueStart = "basicInfo-item value\">";
        private readonly Regex _supRegex = new Regex("<sup>\\[\\d+(-\\d+)?\\]</sup.*?</a>");
        private IDictionary<string, HtmlSource> _basics;
        private IDictionary<string, string> _metas;

        /// <summary>
        /// 简介信息。
        /// </summary>
        public string Introduction
        {
            get
            {
                var html = _source.Substring("<div class=\"lemma-summary\" label-module=\"lemmaSummary\">", "</dd>");
                if (html == null)
                    return null;
                var htmls = html.Split(new[] { "</div><div class=\"para\" label-module=\"para\">" }, StringSplitOptions.RemoveEmptyEntries);
                var list = new List<string>();
                foreach (var item in htmls)
                {
                    HtmlSource current = item.Replace("\r", string.Empty)
                        .Replace("\n", string.Empty);
                    list.Add(current.EscapeHtml());
                }
                html = string.Join("\r\n", list.Select(l => $"<p>{l}</p>"));
                html = html.Replace("&nbsp;", string.Empty);
                return html.Trim();
            }
        }

        /// <summary>
        /// 获取源码中的字符串，不会截断原有代码。
        /// </summary>
        /// <param name="start">开始字符串。</param>
        /// <param name="end">结束字符串。</param>
        /// <returns>返回HTML源码实例。</returns>
        public HtmlSource Substring(string start, string end)
        {
            return _source.Substring(start, end);
        }
        
        /// <summary>
        /// 获取源码中的字符串，不会截断原有代码。
        /// </summary>
        /// <param name="start">开始字符串。</param>
        /// <returns>返回HTML源码实例。</returns>
        public HtmlSource Substring(string start)
        {
            return _source.Substring(start);
        }

        /// <summary>
        /// 当前百科的URL地址。
        /// </summary>
        public string Url => _url;

        /// <summary>
        /// 获取基础信息字符串。
        /// </summary>
        private void GetBasic(string source)
        {
            HtmlSource html = source;
            _metas = html.GetMetas();
            var dic = new Dictionary<string, HtmlSource>(StringComparer.OrdinalIgnoreCase);
            html = html.Substring(BasicStart, "</dl></div>");
            if (html.IsNullOrWhiteSpace)
                return;
            while (!html.IsNullOrWhiteSpace)
            {
                var key = html.Substring(BasicNameStart, "</dt>").EscapeBlank();
                var value = html.Substring(BasicValueStart, "</dd>");
                dic[key] = value.EscapeHtml();
            }
            _basics = dic;
        }

        /// <summary>返回一个循环访问集合的枚举器。</summary>
        /// <returns>用于循环访问集合的枚举数。</returns>
        public IEnumerator<KeyValuePair<string, HtmlSource>> GetEnumerator()
        {
            return (_basics ?? Enumerable.Empty<KeyValuePair<string, HtmlSource>>()).GetEnumerator();
        }

        /// <summary>返回循环访问集合的枚举数。</summary>
        /// <returns>可用于循环访问集合的 <see cref="T:System.Collections.IEnumerator" /> 对象。</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 是否为空。
        /// </summary>
        public bool IsNullOrWhiteSpace => string.IsNullOrWhiteSpace(_source);

        /// <summary>返回表示当前对象的字符串。</summary>
        /// <returns>表示当前对象的字符串。</returns>
        public override string ToString()
        {
            return _source.ToString();
        }
    }
}