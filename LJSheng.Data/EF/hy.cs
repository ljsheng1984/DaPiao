using System;
using System.ComponentModel.DataAnnotations;

namespace LJSheng.Data
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class hy
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public Guid gid { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime addtime { get; set; }

        /// <summary>
        /// 帐号
        /// </summary>
        [Required, MaxLength(50)]
        public string account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required, MaxLength(50)]
        public string pwd { get; set; }

        /// <summary>
        /// 权限[1-用户 2=管理员]
        /// </summary>
        public int qx { get; set; }
    }
}
