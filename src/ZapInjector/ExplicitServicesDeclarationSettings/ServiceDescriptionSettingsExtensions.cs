using System;
using Microsoft.Extensions.DependencyInjection;

namespace ZapInjector.ExplicitServicesDeclarationSettings
{
    public static class ServiceDescriptionSettingsExtensions
    {
        public static ServiceLifetime GetServiceLifetime(this ServiceDescriptionSettings serviceDescriptionSettings)
        {
            switch (serviceDescriptionSettings.ServiceLifetime)
            {
                case "Singleton":
                    return ServiceLifetime.Singleton;
                case "Scoped":
                case null:
                    return ServiceLifetime.Scoped;
                case "Transient":
                    return ServiceLifetime.Transient;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}