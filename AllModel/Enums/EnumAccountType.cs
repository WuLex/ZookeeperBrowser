using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AllModel.Enums
{
    /// <summary>
    /// 账户类型
    /// </summary>
    public enum EnumAccountType
    {
        /// <summary>
        /// 系统操作员
        /// </summary>
        [Description("系统操作员")] [Display(Name = "系统操作员")]
        Admin,

        /// <summary>
        /// 普通管理员
        /// </summary>
        [Description("普通管理员")] [Display(Name = "普通管理员")]
        User
    }
}