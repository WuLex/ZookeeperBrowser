using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AllDto.Common.Auth.Jwt
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Jwt认证
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddJwtAuth(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ISecurityTokenValidator, MyJwtSecurityTokenHandler>();

            //从服务容器中获取自定义令牌验证处理器
            var securityTokenHandler = services.BuildServiceProvider().GetService<MyJwtSecurityTokenHandler>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false, //是否验证超时，MVC版Token保存Cookies时不验证
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                    };

                    //先清除再添加自定义令牌验证器
                    options.SecurityTokenValidators.Clear();
                    options.SecurityTokenValidators.Add(securityTokenHandler);

                    options.Events = new JwtBearerEvents
                    {
                        //重写OnMessageReceived
                        OnMessageReceived = context =>
                        {
                            var authorization = context.Request.Headers["Token"];
                            var token = authorization.FirstOrDefault();
                            if (token != null)
                            {
                                context.Request.Headers.Remove("Authorization");
                                context.Request.Headers.Add("Authorization", $"Bearer {token}");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }
    }
}
