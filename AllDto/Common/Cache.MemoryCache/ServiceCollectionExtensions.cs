using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllDto.Common.Cache.MemoryCache
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Cache缓存
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddCache(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheHandler, MemoryCacheHandler>();
            return services;
        }
    }
}
