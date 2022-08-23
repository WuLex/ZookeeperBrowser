using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AllModel.Enums
{
    /// <summary>
    /// 部门类别
    /// </summary>
    public enum EnumDeptType
    {
        /// <summary>
        /// 年组
        /// </summary>
        [Description("年组")]
        [Display(Name = "年组")]
        grade,
        /// <summary>
        /// 班级
        /// </summary>
        [Description("班级")]
        [Display(Name = "班级")]
        classes
    }
}
