using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CoreAPI.Code.Core
{
    public abstract class AbstractStartup
    {
        public AbstractStartup(IWebHostEnvironment env)
        {
            Env = env;
        }
        protected readonly IWebHostEnvironment Env;

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddWebHost(Env);
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            app.UseWebHost(Env);
        }
    }
}
