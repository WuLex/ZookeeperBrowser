using System.Collections.Generic;
using System.Threading.Tasks;
using ZookeeperBrowser.Common.CommonToolsCore.Attributes;

namespace ZookeeperBrowser.Common.Auth.Jwt
{
    /// <summary>
    /// 权限验证
    /// </summary>
    [Singleton]
    public class PermissionValidateHandler : IPermissionValidateHandler
    {
        private readonly ILoginInfo _loginInfo;

        public PermissionValidateHandler(ILoginInfo loginInfo)
        {
            _loginInfo = loginInfo;
        }

        public async Task<bool> Validate(IDictionary<string, string> routeValues, string httpMethod)
        {
            return await Task.FromResult(true);
        }
    }
}
