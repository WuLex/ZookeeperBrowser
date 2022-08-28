using System;
using System.Linq;
using System.Threading.Tasks;
using AllDto.Common.Auth.Jwt;
using CoreAPI.Common.Auth.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace CoreAPI.Code.Attributes
{
    /// <summary>
    /// 权限验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class PermissionValidateAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //排除匿名访问
            if (context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(AllowAnonymousAttribute)))
                return;

            var config = AuthConfigData.AuthConfig;
            

            //是否启用单账户登录
            if (config.SingleAccount)
            {
            }

            //未登录
            var loginInfo = context.HttpContext.RequestServices.GetService<ILoginInfo>();
            if (loginInfo == null || loginInfo.AccountId == Guid.Empty)
            {
                context.Result = new ChallengeResult();
                return;
            }

            //是否开启权限认证
            if (config.Validate)
            {
                //权限验证- 未实现
                var handler = context.HttpContext.RequestServices.GetService<IPermissionValidateHandler>();

                if (!await handler.Validate(context.ActionDescriptor.RouteValues, context.HttpContext.Request.Method))
                {
                    context.Result = new ForbidResult();
                }
            }
            
        }
    }
}
