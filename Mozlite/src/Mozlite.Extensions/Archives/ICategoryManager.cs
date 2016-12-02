using Microsoft.Extensions.Caching.Memory;
using Mozlite.Core;
using Mozlite.Data;
using Mozlite.Extensions.Categories;

namespace Mozlite.Extensions.Archives
{
    /// <summary>
    /// 分类接口。
    /// </summary>
    public interface ICategoryManager : IComplexCategoryManager<Category>, ISingletonService
    {

    }

    /// <summary>
    /// 分类管理类。
    /// </summary>
    public class CategoryManager : ComplexCategoryManager<Category>, ICategoryManager
    {
        /// <summary>
        /// 初始化类<see cref="CategoryManager"/>。
        /// </summary>
        /// <param name="repository">数据库操作接口。</param>
        /// <param name="cache">缓存接口。</param>
        public CategoryManager(IRepository<Category> repository, IMemoryCache cache) 
            : base(repository, cache)
        {
        }
    }
}