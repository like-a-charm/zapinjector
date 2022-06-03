using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZapInjector.Configurations;
using ZapInjector.Strategies;

namespace ZapInjector
{
    public class LoadServicesDirector
    {
        private readonly IServicesLoaderStrategyFactory _servicesLoaderStrategyFactory;
        private readonly IServicesLoaderConfigurationFactory _servicesLoaderConfigurationFactory;
        private const string RootSectionName = "ZapInjector";

        public LoadServicesDirector(IServicesLoaderStrategyFactory servicesLoaderStrategyFactory, IServicesLoaderConfigurationFactory servicesLoaderConfigurationFactory)
        {
            _servicesLoaderStrategyFactory = servicesLoaderStrategyFactory;
            _servicesLoaderConfigurationFactory = servicesLoaderConfigurationFactory;
        }

        public void LoadServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {            
            configuration
                .GetSection(RootSectionName)
                .GetChildren()
                .ToList()
                .ForEach(configurationChild => LoadServicesForConfigurationChild(serviceCollection, configurationChild));
        }

        public void LoadServicesForConfigurationChild(IServiceCollection serviceCollection, IConfigurationSection configurationChild)
        {
            var servicesLoaderStrategy =
                _servicesLoaderStrategyFactory.CreateFromName(configurationChild["Strategy"]);
            
            var serviceLoaderConfiguration = _servicesLoaderConfigurationFactory.CreateFromType(
                servicesLoaderStrategy.ConfigurationType,
                configurationChild);
            
            servicesLoaderStrategy.LoadServices(serviceCollection, serviceLoaderConfiguration);
        }
    }
}