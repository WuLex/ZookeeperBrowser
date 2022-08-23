using AllModel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AllModel
{
    /// <summary>
    /// 账户表
    /// </summary>
    [Table("Account")]
    public partial class Account : EntityBaseNoDeleted<Guid>
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string PassWord { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public EnumAccountType Type { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        /// <summary>
        /// 激活状态
        /// </summary>
        public EnumStatus Status { get; set; }

        //通过override重写，标记NotMapped特性排除基类属性，不生成表字段
        [NotMapped]
        public override DateTime CreatedTime { get => base.CreatedTime; set => base.CreatedTime = value; }
        [NotMapped]
        public override string OperatorName { get => base.OperatorName; set => base.OperatorName = value; }

    }
}
