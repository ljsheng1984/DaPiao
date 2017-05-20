using System;
using System.ComponentModel.DataAnnotations;

namespace LJSheng.Data
{
    /// <summary>
    /// 点读书本
    /// </summary>
    public class splb
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
        /// 商品名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string name { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public int rmb { get; set; }
    }
}
