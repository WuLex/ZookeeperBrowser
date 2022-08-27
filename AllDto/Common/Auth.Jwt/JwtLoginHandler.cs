using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AllDto.Common.CommonToolsCore.Attributes;
using AllDto.Services.IService;
using AllModel.MyOrm.Result;

namespace AllDto.Common.Auth.Jwt
{
    [Scoped]
    public class JwtLoginHandler : ILoginHandler
    {
        private readonly ILogger _logger;
        private readonly IConfigService _configService;

        public JwtLoginHandler(ILogger<JwtLoginHandler> logger, IConfigService configService)
        {
            _logger = logger;
            _configService = configService;
        }

        public JwtTokenModel Hand(List<Claim> claims, string extendData)
        {
            var _authConfig = new AuthConfigData();
            var config = _configService.GetValue("Auth").GetAwaiter().GetResult() as ResultModel<ConfigDTO>;
            if (config.Success && config.Data.Code.EqualsIgnoreCase("Auth"))
            {
                _authConfig = config.Data.Value.ToJson<AuthConfigData>();
            }
            var jwtConfig = _authConfig.Jwt;

            var token = Build(claims, jwtConfig);

            _logger.LogDebug("生成JwtToken：{token}", token);

            var model = new JwtTokenModel
            {
                AccessToken = token,
                ExpiresIn = jwtConfig.Expires * 60,
                RefreshToken = extendData
            };

            return model;
        }

        private string Build(List<Claim> claims, JwtConfig config)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Key));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(config.Issuer, config.Audience, claims, DateTime.Now, DateTime.Now.AddMinutes(config.Expires), signingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
