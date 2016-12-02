using System;
using Mozlite.Data;

namespace Mozlite.Extensions
{
    /// <summary>
    /// 对象管理基类。
    /// </summary>
    /// <typeparam name="TModel">模型类型。</typeparam>
    public abstract class ObjectManager<TModel> : IObjectManager<TModel> where TModel : ExtendObjectBase
    {
        /// <summary>
        /// 数据库操作接口。
        /// </summary>
        protected readonly IRepository<TModel> Database;
        /// <summary>
        /// 初始化类<see cref="ObjectManager{TModel}"/>。
        /// </summary>
        /// <param name="repository">数据库操作接口。</param>
        protected ObjectManager(IRepository<TModel> repository)
        {
            Database = repository;
        }

        /// <summary>
        /// 判断实例是否重复。
        /// </summary>
        /// <param name="model">模型实例。</param>
        /// <returns>返回判断结果。</returns>
        public virtual bool IsDulicate(TModel model)
        {
            return false;
        }

        /// <summary>
        /// 保存实例。
        /// </summary>
        /// <param name="model">模型实例对象。</param>
        /// <returns>返回执行结果。</returns>
        public virtual DataResult Save(TModel model)
        {
            if (IsDulicate(model))
                return DataAction.Duplicate;
            if (model.Id > 0)
            {
                model.UpdatedDate = DateTime.Now;
                return DataResult.FromResult(Database.Update(model), DataAction.Updated);
            }
            return DataResult.FromResult(Database.Create(model), DataAction.Created);
        }

        /// <summary>
        /// 获取模型实例。
        /// </summary>
        /// <param name="id">实体Id。</param>
        /// <returns>返回模型实例对象。</returns>
        public TModel Get(int id)
        {
            return Database.Find(x => x.Id == id);
        }

        /// <summary>
        /// 获取模型实例。
        /// </summary>
        /// <param name="guid">Guid。</param>
        /// <returns>返回模型实例对象。</returns>
        public TModel Get(Guid guid)
        {
            return Database.Find(x => x.Guid == guid);
        }

        /// <summary>
        /// 获取模型实例。
        /// </summary>
        /// <param name="key">唯一键。</param>
        /// <returns>返回模型实例对象。</returns>
        public TModel Get(string key)
        {
            return Database.Find(x => x.Key == key);
        }

        /// <summary>
        /// 删除实例对象。
        /// </summary>
        /// <param name="id">Id。</param>
        /// <returns>返回删除结果。</returns>
        public virtual DataResult Delete(int id)
        {
            return DataResult.FromResult(Database.Delete(x => x.Id == id), DataAction.Deleted);
        }

        /// <summary>
        /// 删除实例对象。
        /// </summary>
        /// <param name="ids">Id集合。</param>
        /// <returns>返回删除结果。</returns>
        public virtual DataResult Delete(int[] ids)
        {
            return DataResult.FromResult(Database.Delete(x => x.Id.Included(ids)), DataAction.Deleted);
        }

        /// <summary>
        /// 删除实例对象。
        /// </summary>
        /// <param name="ids">Id集合，以“,”分割。</param>
        /// <returns>返回删除结果。</returns>
        public DataResult Delete(string ids)
        {
            return Delete(ids.SplitToInt32());
        }
    }
}