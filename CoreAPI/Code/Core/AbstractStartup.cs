using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CoreAPI.Code.Core
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractStartup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        public AbstractStartup(IWebHostEnvironment env)
        {
            Env = env;
        }


        /// <summary>
        /// 
        /// </summary>
        protected readonly IWebHostEnvironment Env;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddWebHost(Env);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public virtual void Configure(IApplicationBuilder app)
        {
            app.UseWebHost(Env);
        }
    }
}