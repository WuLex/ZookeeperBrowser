
using CoreAPI.Config;

#region ��������
IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();
configuration.GetSection("Setting").Bind(BasicSetting.Setting);
if (BasicSetting.Setting.Urls.IsNull())
    BasicSetting.Setting.Urls = "http://*:5000";

#endregion


//var builder = WebApplication.CreateBuilder(args);
var builder = WebApplication.CreateBuilder(args);

//WebApplicationBuilder ��Ҫ���� 4 �����
//ʹ�� builder.Configuration ������á�
//ʹ�� builder.Services ��ӷ���
//ʹ�� builder.Logging ������־
//���� IHostBuilder �� IWebHostBuilder



#region ���� IHostBuilder �� IWebHostBuilder

#endregion

#region Startup��ѡ
//try
//{
//    builder.WebHost.UseStartup<Startup>();
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//    throw;
//} 
#endregion

#region ��ӷ���
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddWebHost(builder.Environment);
#endregion

#region �ı�˿ڷ�ʽһ
//builder.WebHost.UseUrls("http://localhost:3045");
#endregion

var app = builder.Build();
 
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseWebHost(app.Environment);

//app.UseAuthorization();
//app.MapControllers();

#region �ı�˿ڷ�ʽ��
//app.Run("http://localhost:6054");
#endregion

app.Run();
