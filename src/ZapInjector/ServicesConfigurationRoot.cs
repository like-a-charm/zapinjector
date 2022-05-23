using System;
using Microsoft.Extensions.DependencyInjection;
using ZapInjector.Configurations;
using ZapInjector.Strategies;
using ZapInjector.Strategies.Conventions;
using ZapInjector.Utils;

namespace ZapInjector
{
    public static class ServicesConfigurationRoot
    {
        public static IServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddSingleton<LoadServicesDirector>();
            services.AddSingleton<IServicesLoaderStrategyFactory, ServicesLoaderStrategyFactory>();
            services.AddSingleton<IServicesLoaderConfigurationFactory, ServicesLoaderConfigurationFactory>();
            services.AddSingleton<IAssembliesExportedTypesAccessor, AssembliesExportedTypesAccessor>();
            services
                .AddSingleton<ILoadAllServicesFromAssembliesConventionFactory,
                    LoadAllServicesFromAssembliesConventionFactory>();
            return services.BuildServiceProvider();
        }
    }
}