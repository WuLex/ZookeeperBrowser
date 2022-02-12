using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZookeeperBrowser.Model.Code;
using ZookeeperBrowser.Model.Enums;

namespace ZookeeperBrowser.Model
{
    /// <summary>
    /// 学生信息表
    /// </summary>
    [Table("StudentInfo")]
    public partial class StudentInfo: EntityBase<long>
    {
        /// <summary>
        /// 学生姓名
        /// </summary>
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }
        /// <summary>
        /// 学生性别
        /// </summary>
        public EnumGender Gender { get; set; }
        /// <summary>
        /// 学生民族
        /// </summary>
        public EnumNation Nation { get; set; } = EnumNation.hanzu;
        /// <summary>
        /// 入学时间
        /// </summary>
        public DateTime EnrollmentDT { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public int DepartId { get; set; }
        /// <summary>
        /// 学生电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 学生邮箱
        /// </summary>
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        [Required]
        public string IdentityCard { get; set; }
        /// <summary>
        /// 学生地址
        /// </summary>
        [Column(TypeName = "varchar(200)")]
        public string Address { get; set; }
        /// <summary>
        /// 学生照片
        /// </summary>
        public string Photos { get; set; }
        /// <summary>
        /// 激活状态
        /// </summary>
        public EnumStatus Status { get; set; }
        /// <summary>
        /// 学生部门
        /// </summary>
        [ForeignKey("DepartId")]
        public virtual Depart Depart { get; set; }
    }
}
