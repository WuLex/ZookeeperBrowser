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
    /// 配置表
    /// </summary>
    [Table("Config")]
    public partial class Config : EntityBaseNoDeleted
    {
        /// <summary>
        /// 配置代码
        /// </summary>
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Code { get; set; }

        /// <summary>
        /// 配置脚本
        /// </summary>
        [Column(TypeName = "varchar(1024)")]
        public string Value { get; set; }
    }
}
