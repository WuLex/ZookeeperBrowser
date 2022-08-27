using AllModel.Code;
using AllModel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AllModel
{
    /// <summary>
    /// 认证信息表
    /// </summary>
    [Table("AuthInfo")]
    [MetadataType(typeof(EntityBaseNoDeleted))]
    public partial class AuthInfo : EntityBaseNoDeleted
    {
        /// <summary>
        /// 账户编号
        /// </summary>
        [Required]
        public Guid AccountId { get; set; }

        /// <summary>
        /// 登录平台
        /// </summary>
        public EnumPlatform Platform { get; set; }

        /// <summary>
        /// 刷新令牌
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// 刷新令牌过期时间
        /// </summary>
        public DateTime RefreshTokenExpiredTime { get; set; }

        /// <summary>
        /// 最后登录时间戳
        /// </summary>
        public long LoginTime { get; set; }

        /// <summary>
        /// 最后登录IP
        /// </summary>
        public string LoginIP { get; set; } = string.Empty;

        //通过override重写，标记NotMapped特性排除基类属性，不生成表字段
        //[NotMapped]
        //public override DateTime CreatedTime { get => base.CreatedTime; set => base.CreatedTime = value; }
        //[NotMapped]
        //public override string OperatorName { get => base.OperatorName; set => base.OperatorName = value; }

        //[NotMapped]
        //public new  int Deleted { get; set; }

        //[NotMapped]
        //public new DateTime CreatedTime { get ; set ; }
       
        //[NotMapped]
        //public new string OperatorName { get ; set; }
    }
}
