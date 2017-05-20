using System;
using System.ComponentModel.DataAnnotations;

namespace LJSheng.Data
{
    /// <summary>
    /// 分类
    /// </summary>
    public class fl
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public Guid gid { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime addtime { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int sort { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public int show { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string name { get; set; }
    }
}
