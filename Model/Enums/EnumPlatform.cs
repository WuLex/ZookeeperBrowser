using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZookeeperBrowser.Model.Enums
{
    /// <summary>
    /// 平台类型
    /// </summary>
    public enum EnumPlatform
    {
        [Description("未知")]
        [Display(Name = "未知")]
        UnKnown = -1,
        /// <summary>
        /// Web
        /// </summary>
        [Description("Web")]
        [Display(Name = "Web")]
        Web,
        /// <summary>
        /// Mobile
        /// </summary>
        [Description("手机")]
        [Display(Name = "手机")]
        Mobile,
        /// <summary>
        /// WeChat
        /// </summary>
        [Description("微信")]
        [Display(Name = "微信")]
        WeChat
    }
}
