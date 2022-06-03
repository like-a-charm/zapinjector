using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace ZapInjector.ExplicitServicesDeclarationSettings
{
    public class ServiceFactory
    {
        public ObjectFactory? ObjectFactory { get; set; }
        public Type? Type { get; set; }
        public IEnumerable<ServiceFactory>? DependenciesFactories { get; set; }
    }
}