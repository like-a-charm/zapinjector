using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZapInjector.Strategies;

namespace ZapInjector.Configurations
{
    public class ServicesLoaderConfigurationFactory: IServicesLoaderConfigurationFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ServicesLoaderConfigurationFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IServicesLoaderConfiguration CreateFromType(Type configurationType, IConfiguration configuration)
        {
            var configurationInstance = ActivatorUtilities.CreateInstance(_serviceProvider, configurationType) ??
                                throw new InvalidOperationException();
            
            configuration.Bind("Configuration", (IServicesLoaderConfiguration) configurationInstance);

            return (IServicesLoaderConfiguration)configurationInstance;
        }
    }
}