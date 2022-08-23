using AllModel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AllDto
{
    [Description("认证信息")]
    public class AuthInfoDTO
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Description("编号")]
        public int Id { get; set; }

        /// <summary>
        /// 账户编号
        /// </summary>
        [Description("账户编号")]
        public Guid AccountId { get; set; }

        /// <summary>
        /// 登录平台
        /// </summary>
        [Description("登录平台")]
        public EnumPlatform Platform { get; set; }

        /// <summary>
        /// 刷新令牌
        /// </summary>
        [Description("刷新令牌")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// 刷新令牌过期时间
        /// </summary>
        [Description("刷新令牌过期时间")]
        public DateTime RefreshTokenExpiredTime { get; set; }

        /// <summary>
        /// 最后登录时间戳
        /// </summary>
        [Description("最后登录时间戳")]
        public long LoginTime { get; set; }

        /// <summary>
        /// 最后登录IP
        /// </summary>
        [Description("最后登录IP")]
        public string LoginIP { get; set; } = string.Empty;

    }
}
