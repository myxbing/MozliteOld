using Microsoft.Extensions.Caching.Memory;
using Mozlite.Core;
using Mozlite.Data;
using Mozlite.Extensions.Categories;

namespace Mozlite.Extensions.Tags
{
    /// <summary>
    /// 分类管理接口。
    /// </summary>
    public interface ICategoryManager : ICategoryManager<Category>, ISingletonService
    {

    }

    /// <summary>
    /// 分类管理类型。
    /// </summary>
    public class CategoryManager : CategoryManager<Category>, ICategoryManager
    {
        /// <summary>
        /// 初始化类<see cref="CategoryManager"/>。
        /// </summary>
        /// <param name="repository">类型数据库操作接口。</param>
        /// <param name="cache">换成接口。</param>
        public CategoryManager(IRepository<Category> repository, IMemoryCache cache)
            : base(repository, cache)
        {
        }
    }
}