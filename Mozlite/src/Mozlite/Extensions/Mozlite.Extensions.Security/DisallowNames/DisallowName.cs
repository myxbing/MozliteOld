using Mozlite.Data.Metadata;

namespace Mozlite.Extensions.Security.DisallowNames
{
    /// <summary>
    /// 非法用户名。
    /// </summary>
    [Table("Users_DisallowNames")]
    public class DisallowName
    {
        /// <summary>
        /// 唯一Id。
        /// </summary>
        [Identity]
        public int Id { get; set; }

        /// <summary>
        /// 非法用户名。
        /// </summary>
        [Key]
        [Size(20)]
        public string Name { get; set; }
    }
}