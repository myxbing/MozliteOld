using System;
using Mozlite.Data.Metadata;

namespace Mozlite.Extensions
{
    /// <summary>
    /// 可扩展实体基类。
    /// </summary>
    public abstract class ExtendObjectBase : ExtendBase, IObject
    {
        /// <summary>
        /// 全局唯一Id。
        /// </summary>
        public Guid Guid { get; set; } = Guid.NewGuid();
        
        /// <summary>
        /// 唯一键，一般用于URL查询，建立索引。
        /// </summary>
        [Size(64)]
        public string Key { get; set; }

        /// <summary>
        /// 状态。
        /// </summary>
        public ObjectStatus Status { get; set; }

        /// <summary>
        /// 添加时间。
        /// </summary>
        [Ignore(Ignore.Update)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新时间。
        /// </summary>
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}