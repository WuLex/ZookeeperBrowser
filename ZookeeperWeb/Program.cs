using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;
using WebApiClient;
using ZookeeperBrowser.Code;
using ZookeeperBrowser.Services;
using ZookeeperBrowser.Settings;
using ZookeeperBrowser.Utils;

#region 获取配置信息

var configuration = new ConfigurationBuilder()
               .SetBasePath(Environment.CurrentDirectory)
               .AddJsonFile("appsettings.json")
               .Build();
configuration.GetSection("Setting").Bind(AppSetting.Setting);

if (AppSetting.Setting.Urls.IsNull())
{
    AppSetting.Setting.Urls = "http://*:8080";
}

#endregion 获取配置信息

var builder = WebApplication.CreateBuilder(args);

#region 注册各种服务

// Add services to the container.

builder.Services.AddControllersWithViews().AddNewtonsoftJson();
//使用Session
builder.Services.AddSession();

builder.Services.AddSingleton<IZookeeperService, ZookeeperService>();
//builder.Services.AddScoped<IMyDependency2, MyDependency2>();
builder.Services.AddSingleton<ZooKeeperManager>(ZooKeeperManager.Instance);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<ILoginInfo, LoginInfo>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
  .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
  {
      options.LoginPath = new PathString("/Login/Index");
      options.LogoutPath = new PathString("/Login/Logout");
      options.AccessDeniedPath = new PathString("/Home/Error");
      options.Cookie.Name = "_AdminTicketCookie";
      //options.Cookie.SameSite = SameSiteMode.None;

      //当Cookie 过期时间已达一半时，是否重置为ExpireTimeSpan
      options.SlidingExpiration = true;
      options.Cookie.HttpOnly = true;
  });

//添加HttpClient相关
var types = typeof(Program).Assembly.GetTypes()
            .Where(type => type.IsInterface
            && ((System.Reflection.TypeInfo)type).ImplementedInterfaces != null
            && type.GetInterfaces().Any(a => a.FullName == typeof(IHttpApi).FullName));
foreach (var type in types)
{
    builder.Services.AddHttpApi(type);
    builder.Services.ConfigureHttpApi(type, o =>
    {
        o.HttpHost = new Uri(AppSetting.Setting.ApiUrl);
    });
}

#endregion 

#region 添加swagger注释

var basePath = AppContext.BaseDirectory;

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ZooKeeper.Admin.Api"
    });

    #region 暂未使用

    //var xmlPath = Path.Combine(basePath, "NET6.Api.xml");
    //c.IncludeXmlComments(xmlPath, true);
    //var xmlDomainPath = Path.Combine(basePath, "NET6.Domain.xml");
    //c.IncludeXmlComments(xmlDomainPath, true);
    //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    //{
    //    Description = "Value: Bearer {token}",
    //    Name = "Authorization",
    //    In = ParameterLocation.Header,
    //    Type = SecuritySchemeType.ApiKey,
    //    Scheme = "Bearer"
    //});
    //c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    //{
    //  {
    //    new OpenApiSecurityScheme
    //    {
    //      Reference = new OpenApiReference
    //      {
    //        Type = ReferenceType.SecurityScheme,
    //        Id = "Bearer"
    //      },Scheme = "oauth2",Name = "Bearer",In = ParameterLocation.Header,
    //    },new List<string>()
    //  }
    //});

    #endregion 暂未使用
});

#endregion 添加swagger注释

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseCors(p =>
{
    p.AllowAnyOrigin();
    p.WithMethods("GET");
    p.AllowAnyHeader();
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

#region 启用swaggerUI

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ZooKeeper.Admin.Api V1Docs");
    //c.RoutePrefix = string.Empty;
    //c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    //c.DefaultModelsExpandDepth(-1);
});

#endregion 启用swaggerUI

app.Run();