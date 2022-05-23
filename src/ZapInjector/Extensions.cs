using Microsoft.Extensions.Configuration;
using ZapInjector;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {
        public static IServiceCollection LoadFromConfiguration(this IServiceCollection services,
            IConfiguration configurationRoot)
        {
            ServicesConfigurationRoot
                .BuildServiceProvider()
                .GetRequiredService<LoadServicesDirector>()
                .LoadServices(services, configurationRoot);
            return services;
        }
    }
}