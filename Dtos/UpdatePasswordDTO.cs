using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Student.DTO
{
    [Description("账户修改密码")]
    public class UpdatePasswordDTO
    {
        /// <summary>
        /// 账户编号
        /// </summary>
        [JsonIgnore]
        public Guid AccountId { get; set; }

        /// <summary>
        /// 旧密码
        /// </summary>
        [Display(Name = "旧密码")]
        [Required(ErrorMessage = "请输入旧密码")]
        public string OldPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [Display(Name = "新密码")]
        [Required(ErrorMessage = "请输入新密码")]
        public string NewPassword { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [Display(Name = "确认密码")]
        [Compare("NewPassword", ErrorMessage = "新密码和确认密码不匹配")]
        public string ConfirmPassword { get; set; }
    }
}
