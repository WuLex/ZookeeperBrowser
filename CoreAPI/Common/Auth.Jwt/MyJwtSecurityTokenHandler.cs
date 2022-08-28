using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AllModel.MyOrm.Result;
using AllDto.Common.Auth.Jwt;
using AllDto;
using CoreAPI.Services.IService;

namespace CoreAPI.Common.Auth.Jwt
{
    /// <summary>
    /// 因为签名的key是动态的，所以需要自定义jwt令牌验证处理器
    /// </summary>
    public class MyJwtSecurityTokenHandler : JwtSecurityTokenHandler, ISecurityTokenValidator
    {
        private readonly IConfigService _configService;
        public MyJwtSecurityTokenHandler(IConfigService configService)
        {
            _configService = configService;
        }
        public override ClaimsPrincipal ValidateToken(string token, TokenValidationParameters validationParameters,
            out SecurityToken validatedToken)
        {
            var _authConfig = new AuthConfigData();
            var config = _configService.GetValue("Auth").GetAwaiter().GetResult() as ResultModel<ConfigDTO>;
            if (config.Success && config.Data.Code.EqualsIgnoreCase("Auth"))
            {
                _authConfig = config.Data.Value.ToJson<AuthConfigData>();
            }
            var jwtConfig = _authConfig.Jwt;

            validationParameters.ValidIssuer = jwtConfig.Issuer;
            validationParameters.ValidAudience = jwtConfig.Audience;
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key));

            return base.ValidateToken(token, validationParameters, out validatedToken);
        }
    }
}
