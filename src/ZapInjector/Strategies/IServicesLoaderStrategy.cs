using System;
using Microsoft.Extensions.DependencyInjection;
using ZapInjector.Configurations;

namespace ZapInjector.Strategies
{
    public interface IServicesLoaderStrategy
    {
        Type ConfigurationType { get; }
        void LoadServices(IServiceCollection services, object configuration);
    }
    public interface IServicesLoaderStrategy<in T>: IServicesLoaderStrategy where T: IServicesLoaderConfiguration
    {
        void LoadServices(IServiceCollection services, T configuration);
    }
}