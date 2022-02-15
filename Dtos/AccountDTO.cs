using Microsoft.AspNetCore.Http;
using Student.DTO.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ZookeeperBrowser.Model.Enums;

namespace Student.DTO
{
    [Description("账户信息")]
    public class AccountDTO
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Display(Name = "编号")]
        public Guid Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [Display(Name = "账号")]
        [Required(ErrorMessage = "{0},不能为空")]
        [StringLength(18, ErrorMessage = "{0},不能大于{1}")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        [StringLength(11, ErrorMessage = "{0},不能小于{2}，最长{1}", MinimumLength = 6)]
        public string PassWord { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("PassWord", ErrorMessage = "密码和确认密码不匹配")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Display(Name = "类型")]
        public EnumAccountType Type { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Display(Name = "昵称")]
        [Required(ErrorMessage = "{0},不能为空")]
        [StringLength(7, ErrorMessage = "{0},不能大于{1}")]
        public string Name { get; set; }

        /// <summary>
        /// 激活状态
        /// </summary>
        [Display(Name = "激活状态")]
        public EnumStatus Status { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Display(Name = "修改时间")]
        [IgnoreProperty]
        public string ModifiedTime { get; set; }

        /// <summary>
        /// 类型展示
        /// </summary>
        [IgnoreProperty]
        public string TypeName { get; set; }
        /// <summary>
        /// 激活状态展示
        /// </summary>
        [IgnoreProperty]
        public string StatusName { get; set; }

    }
}
