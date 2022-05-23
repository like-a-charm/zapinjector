using System;
using Microsoft.Extensions.DependencyInjection;
using ZapInjector.Configurations;

namespace ZapInjector.Strategies
{
    public abstract class ServicesLoaderStrategyBase<T>: IServicesLoaderStrategy<T> where T:IServicesLoaderConfiguration
    {
        public abstract void LoadServices(IServiceCollection services, T configuration);

        public Type ConfigurationType => typeof(T);
        
        public void LoadServices(IServiceCollection services, object configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (configuration is not T defaultConfiguration)
            {
                throw new ArgumentException(
                    $"type of configuration should be compatible with {typeof(T)} while it's {configuration.GetType()}");
            }
            
            LoadServices(services, defaultConfiguration);
        }
    }
}