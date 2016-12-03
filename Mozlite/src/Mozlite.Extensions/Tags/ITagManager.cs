using System.Collections.Generic;
using Mozlite.Core;

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
        /// 获取标签。
        /// </summary>
        /// <param name="id">标签Id。</param>
        /// <returns>返回标签实例。</returns>
        Tag Get(int id);
    }
}