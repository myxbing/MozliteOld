using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Mozlite.Core;
using Mozlite.Data;

namespace Mozlite.Extensions.Tags
{
    /// <summary>
    /// 标签管理接口。
    /// </summary>
    public interface ITagManager : ISingletonService
    {
        /// <summary>
        /// 保存标签。
        /// </summary>
        /// <param name="tag">标签实例。</param>
        /// <returns>返回保存结果。</returns>
        DataResult Save(Tag tag);

        /// <summary>
        /// 删除标签。
        /// </summary>
        /// <param name="ids">标签Id集合。</param>
        /// <returns>返回删除结果。</returns>
        DataResult Delete(string ids);

        /// <summary>
        /// 删除标签。
        /// </summary>
        /// <param name="ids">标签Id集合。</param>
        /// <returns>返回删除结果。</returns>
        DataResult Delete(int[] ids);

        /// <summary>
        /// 删除标签。
        /// </summary>
        /// <param name="id">标签Id。</param>
        /// <returns>返回删除结果。</returns>
        DataResult Delete(int id);

        /// <summary>
        /// 获取热门的标签。
        /// </summary>
        /// <param name="categoryId">分类Id。</param>
        /// <returns>返回标签列表。</returns>
        IEnumerable<Tag> Load(int categoryId);

        /// <summary>
        /// 分页加载标签。
        /// </summary>
        /// <typeparam name="TQuery">查询实例类型。</typeparam>
        /// <param name="query">查询实例。</param>
        /// <returns>返回分页标签列表。</returns>
        TQuery Load<TQuery>(TQuery query) where TQuery : QueryBase<Tag>;

        /// <summary>
        /// 获取标签。
        /// </summary>
        /// <param name="id">标签Id。</param>
        /// <returns>返回标签实例。</returns>
        Tag Get(int id);

        /// <summary>
        /// 保存内容详情。
        /// </summary>
        /// <param name="id">标签Id。</param>
        /// <param name="body">内容详情。</param>
        /// <returns>返回保存结果。</returns>
        DataResult SaveBody(int id, string body);

        /// <summary>
        /// 上传图标并返回图标地址。
        /// </summary>
        /// <param name="file">文件实例。</param>
        /// <returns>返回图标地址。</returns>
        Task<string> UploadIconAsync(IFormFile file);
    }
}