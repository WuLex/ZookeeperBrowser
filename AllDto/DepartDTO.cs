using AllDto.Attributes;
using AllModel;
using AllModel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AllDto
{
    [Description("部门")]
    public class DepartDTO
    {
        /// <summary>
        /// 部门编号
        /// </summary>
        [Display(Name = "部门编号")]
        public int Id { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [Display(Name = "部门名称")]
        [Required(ErrorMessage = "{0} 不能为空")]
        public string DepartName { get; set; }
        /// <summary>
        /// 部门类别
        /// </summary>
        [Display(Name = "部门类别")]
        public EnumDeptType DeptType { get; set; }
        /// <summary>
        /// 年组编号
        /// </summary>
        [IgnoreProperty]
        public int? GradeId { get; set; }

        /// <summary>
        /// 部门类别展示
        /// </summary>
        [IgnoreProperty]
        public string DeptTypeName { get; set; }

        /// <summary>
        /// 年组名称展示
        /// </summary>
        [IgnoreProperty]
        public string GradeName { get; set; }
    }
}
