using System;
using System.Collections.Generic;
using System.Text;

namespace AllDto.Common.Auth.Jwt
{
    /// <summary>
    /// JWT令牌
    /// </summary>
    public class JwtTokenModel
    {
        /// <summary>
        /// jwt令牌
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 刷新令牌
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// 有效期(秒)
        /// </summary>
        public int ExpiresIn { get; set; }

        /// <summary>
        /// 登录账号名称
        /// </summary>
        public string AccountName { get; set; }
    }
}
