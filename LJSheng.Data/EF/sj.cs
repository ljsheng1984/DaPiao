using System;
using System.ComponentModel.DataAnnotations;

namespace LJSheng.Data
{
    /// <summary>
    /// 商家
    /// </summary>
    public class sj
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
        /// 商家名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string name { get; set; }

        /// <summary>
        /// 商家地址
        /// </summary>
        [StringLength(200)]
        public string address { get; set; }

        /// <summary>
        /// 商家联系方式
        /// </summary>
        [StringLength(200)]
        public string lxfs { get; set; }

        /// <summary>
        /// 商户编号
        /// </summary>
        [StringLength(200)]
        public string free_path { get; set; }
    }
}
