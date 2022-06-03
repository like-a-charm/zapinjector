using System;
using Microsoft.Extensions.DependencyInjection;
using ZapInjector.Configurations;
using ZapInjector.ExplicitServicesDeclarationSettings;
using ZapInjector.ExplicitServicesDeclarationSettings.Validators;
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
            services.AddSingleton<IExplicitServiceDeclarationSettingsVisitorFactory, ExplicitServiceDeclarationSettingsVisitorFactory>();
            services.AddSingleton(ValueSettingsValidator.Instance);
            services.AddSingleton(ServiceDescriptionSettingsValidator.Instance);
            return services.BuildServiceProvider();
        }
    }
}