using System;

namespace ZapInjector.Strategies.Conventions
{
    public interface ILoadAllServicesFromAssembliesConventionFactory
    {
        ILoadAllServicesFromAssembliesConvention CreateFromConventionName(string conventionName);
    }
}