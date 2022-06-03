using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace ZapInjector.ExplicitServicesDeclarationSettings
{
    public static class ObjectFactoryInfoExtensions
    {
        public static object CreateInstance(this ServiceFactory serviceFactory, IServiceProvider serviceProvider)
        {
            var dependencies = Array.Empty<object>();
            if (serviceFactory.DependenciesFactories?.Any() ?? false)
            {
                dependencies = serviceFactory.DependenciesFactories
                    .Select(x => x.CreateInstance(serviceProvider)).ToArray();
            }

            return serviceFactory.ObjectFactory!.Invoke(serviceProvider, dependencies);
        }

        public static ServiceDescriptor ToServiceDescriptor(this ServiceFactory serviceFactory,
            ServiceLifetime serviceLifetime)
        {
            return new ServiceDescriptor(
                serviceFactory.Type!,
                serviceFactory.CreateInstance,
                serviceLifetime
            );
        }
    }
}