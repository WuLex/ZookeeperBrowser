using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AllModel.Enums
{
    /// <summary>
    /// 激活状态
    /// </summary>
    public enum EnumStatus
    {
        /// <summary>
        /// 未激活
        /// </summary>
        [Description("未激活")] [Display(Name = "未激活")]
        Inactive,

        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")] [Display(Name = "正常")]
        Enabled,

        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")] [Display(Name = "禁用")]
        Disabled,

        /// <summary>
        /// 注销
        /// </summary>
        [Description("注销")] [Display(Name = "注销")]
        Closed
    }
}