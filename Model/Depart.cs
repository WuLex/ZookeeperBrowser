using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ZookeeperBrowser.Model.Code;
using ZookeeperBrowser.Model.Enums;

namespace ZookeeperBrowser.Model
{
    /// <summary>
    /// 部门表
    /// </summary>
    [Table("Depart")]
    public partial class Depart : EntityBaseNoDeleted
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string DepartName { get; set; }

        /// <summary>
        /// 部门类别
        /// </summary>
        public EnumDeptType DeptType { get; set; }

        /// <summary>
        /// 年组编号
        /// </summary>
        public int? GradeId { get; set; } = null;

        /// <summary>
        /// 年组信息
        /// </summary>
        [ForeignKey("GradeId")]
        public virtual Depart GradeDepart { get; set; }
    }
}
