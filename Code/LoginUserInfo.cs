using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZookeeperBrowser.Common.CommonToolsCore.Extensions;

namespace ZookeeperBrowser.Code
{
    public class LoginUserInfo : ILoginUserInfo
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public LoginUserInfo(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string AccessToken 
        {
            get
            {
                var accessToken = _contextAccessor?.HttpContext?.User?.FindFirst("AccessToken");

                if (accessToken == null || accessToken.Value.IsNull())
                {
                    return "";
                }
                return accessToken.Value;
            }
        }

        public string RefreshToken
        {
            get
            {
                var refreshToken = _contextAccessor?.HttpContext?.User?.FindFirst("RefreshToken");

                if (refreshToken == null || refreshToken.Value.IsNull())
                {
                    return "";
                }
                return refreshToken.Value;
            }
        }

        public string ExpiresIn
        {
            get
            {
                var expiresIn = _contextAccessor?.HttpContext?.User?.FindFirst("ExpiresIn");

                if (expiresIn == null || expiresIn.Value.IsNull())
                {
                    return "";
                }
                return expiresIn.Value;
            }
        }
    }
}
