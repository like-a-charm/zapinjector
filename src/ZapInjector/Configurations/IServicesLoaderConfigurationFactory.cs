using System;
using Microsoft.Extensions.Configuration;

namespace ZapInjector.Configurations
{
    public interface IServicesLoaderConfigurationFactory
    {
        IServicesLoaderConfiguration
            CreateFromType(Type configurationType, IConfiguration configuration);
    }
}