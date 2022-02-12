using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZookeeperBrowser.Model.Enums
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum EnumGender
    {
        [Description("未知")]
        [Display(Name = "未知")]
        UnKnown = -1,
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        [Display(Name = "男")]
        Man,
        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        [Display(Name = "女")]
        Woman
    }
}
