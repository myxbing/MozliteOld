using System;

namespace Mozlite.Extensions
{
    /// <summary>
    /// 对象服务接口。
    /// </summary>
    /// <typeparam name="TModel">模型类型。</typeparam>
    public interface IObjectManager<TModel>
        where TModel : IObject
    {
        /// <summary>
        /// 判断实例是否重复。
        /// </summary>
        /// <param name="model">模型实例。</param>
        /// <returns>返回判断结果。</returns>
        bool IsDulicate(TModel model);

        /// <summary>
        /// 保存实例。
        /// </summary>
        /// <param name="model">模型实例对象。</param>
        /// <returns>返回执行结果。</returns>
        DataResult Save(TModel model);

        /// <summary>
        /// 获取模型实例。
        /// </summary>
        /// <param name="id">实体Id。</param>
        /// <returns>返回模型实例对象。</returns>
        TModel Get(int id);

        /// <summary>
        /// 获取模型实例。
        /// </summary>
        /// <param name="guid">Guid。</param>
        /// <returns>返回模型实例对象。</returns>
        TModel Get(Guid guid);

        /// <summary>
        /// 获取模型实例。
        /// </summary>
        /// <param name="key">唯一键。</param>
        /// <returns>返回模型实例对象。</returns>
        TModel Get(string key);

        /// <summary>
        /// 删除实例对象。
        /// </summary>
        /// <param name="id">Id。</param>
        /// <returns>返回删除结果。</returns>
        DataResult Delete(int id);

        /// <summary>
        /// 删除实例对象。
        /// </summary>
        /// <param name="ids">Id集合。</param>
        /// <returns>返回删除结果。</returns>
        DataResult Delete(int[] ids);

        /// <summary>
        /// 删除实例对象。
        /// </summary>
        /// <param name="ids">Id集合，以“,”分割。</param>
        /// <returns>返回删除结果。</returns>
        DataResult Delete(string ids);
    }
}