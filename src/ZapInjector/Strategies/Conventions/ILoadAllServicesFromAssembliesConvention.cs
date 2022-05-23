using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace ZapInjector.Strategies.Conventions
{
    public interface ILoadAllServicesFromAssembliesConvention
    {
        IEnumerable<Type> GetAllTypesToRegisterForService(Type service, IEnumerable<Type> allTypes);
        ServiceLifetime DefaultServiceLifetime { get; }
    }
}