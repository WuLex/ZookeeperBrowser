using Student.DTO.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ZookeeperBrowser.Model.Enums;

namespace Student.DTO.Login
{
    public class LoginModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "请输入用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public VerifyCodeModel VerifyCode { get; set; }

        /// <summary>
        /// 平台
        /// </summary>
        public EnumPlatform Platform { get; set; }

        [IgnoreProperty]
        public string IP { get; set; }

        [IgnoreProperty]
        public string UserAgent { get; set; }
    }
}
