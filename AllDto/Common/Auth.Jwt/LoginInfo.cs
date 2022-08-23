using AllDto.Common.yrjw.CommonToolsCore.Attributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllDto.Common.Auth.Jwt
{
    [Singleton]
    public class LoginInfo : ILoginInfo
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public LoginInfo(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public Guid AccountId
        {
            get
            {
                var accountId = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.AccountId);

                if (accountId != null && accountId.Value.NotNull())
                {
                    return new Guid(accountId.Value);
                }
                return Guid.Empty;
            }
        }

        public string AccountName
        {
            get
            {
                var accountName = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.AccountName);

                if (accountName == null || accountName.Value.IsNull())
                {
                    return "";
                }
                return accountName.Value;
            }
        }
        public int AccountType 
        {
            get
            {
                var pt = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.Platform);
                if (pt != null && pt.Value.NotNull())
                {
                    return pt.Value.ToInt();
                }

                return -1;
            }
        }

        public int Platform
        {
            get
            {
                var ty = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.AccountType);

                if (ty != null && ty.Value.NotNull())
                {
                    return ty.Value.ToInt();
                }

                return -1;
            }
        }

        public string IP
        {
            get
            {
                if (_contextAccessor?.HttpContext?.Connection == null)
                    return "";

                return _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }
        }

        public string IPv4 
        {
            get
            {
                if (_contextAccessor?.HttpContext?.Connection == null)
                    return "";

                return _contextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
        }

        public string IPv6
        {
            get
            {
                if (_contextAccessor?.HttpContext?.Connection == null)
                    return "";

                return _contextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv6().ToString();
            }
        }

        public long LoginTime
        {
            get
            {
                var ty = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.LoginTime);

                if (ty != null && ty.Value.NotNull())
                {
                    return ty.Value.ToLong();
                }

                return 0L;
            }
        }

        public string UserAgent
        {
            get
            {
                if (_contextAccessor?.HttpContext?.Request == null)
                    return "";

                return _contextAccessor.HttpContext.Request.Headers["User-Agent"];
            }
        }
    }
}
