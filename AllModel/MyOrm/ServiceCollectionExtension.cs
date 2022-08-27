﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllModel.MyOrm;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddChimp(this IServiceCollection services,
            Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContext<BaseDbContext>(options);
            services.AddScoped<DbContext, BaseDbContext>();
            AddDefault(services);
            return services;
        }
        public static IServiceCollection AddChimp<T>(this IServiceCollection services,
            Action<DbContextOptionsBuilder> options) where T : BaseDbContext
        {
            services.AddDbContext<T>(options);
            services.AddScoped<DbContext, T>();
            AddDefault(services);
            return services;
        }

        #region private function
        private static void AddDefault(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AutoDi();
            services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>));
        }
        //auto di
        private static IServiceCollection AutoDi(this IServiceCollection services)
        {
            var allAssemblies = AppDomain.CurrentDomain.GetCurrentPathAssembly();
            foreach (var assembly in allAssemblies)
            {
                var types = assembly.GetTypes()
                    .Where(type => type.IsClass
                                   && type.BaseType != null
                                   && type.HasImplementedRawGeneric(typeof(IDependency)));
                foreach (var type in types)
                {
                    var interfaces = type.GetInterfaces();

                    var interfaceType = interfaces.FirstOrDefault(x => x.Name == $"I{type.Name}");
                    if (interfaceType == null)
                    {
                        interfaceType = type;
                    }
                    ServiceDescriptor serviceDescriptor =
                        new ServiceDescriptor(interfaceType, type, ServiceLifetime.Scoped);
                    if (!services.Contains(serviceDescriptor))
                    {
                        services.Add(serviceDescriptor);
                    }
                }
            }

            return services;
        }
        #endregion
    }
}