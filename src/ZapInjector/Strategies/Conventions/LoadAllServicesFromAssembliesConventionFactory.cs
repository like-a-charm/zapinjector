using System;
using Microsoft.Extensions.DependencyInjection;

namespace ZapInjector.Strategies.Conventions
{
    public class LoadAllServicesFromAssembliesConventionFactory: ILoadAllServicesFromAssembliesConventionFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public LoadAllServicesFromAssembliesConventionFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ILoadAllServicesFromAssembliesConvention CreateFromConventionName(string conventionName)
        {
            var conventionToCreate = Type.GetType(conventionName);

            if (conventionToCreate == null)
            {
                throw new ArgumentException($"Could not activate any convention from {conventionName}");
            }
            
            return (ILoadAllServicesFromAssembliesConvention) ActivatorUtilities.CreateInstance(_serviceProvider, conventionToCreate) ?? throw new InvalidOperationException();
        }
    }
}