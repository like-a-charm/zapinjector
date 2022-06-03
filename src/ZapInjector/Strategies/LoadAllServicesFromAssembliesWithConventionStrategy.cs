using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using ZapInjector.Configurations;
using ZapInjector.Strategies.Conventions;
using ZapInjector.Utils;

namespace ZapInjector.Strategies
{
    public class LoadAllServicesFromAssembliesWithConventionStrategy: ServicesLoaderStrategyBase<LoadAllServicesFromAssembliesWithConventionConfiguration>
    {
        private readonly ILoadAllServicesFromAssembliesConventionFactory
            _loadAllServicesFromAssembliesConventionFactory;

        private readonly IAssembliesExportedTypesAccessor _assembliesExportedTypesAccessor;

        public LoadAllServicesFromAssembliesWithConventionStrategy(ILoadAllServicesFromAssembliesConventionFactory loadAllServicesFromAssembliesConventionFactory,
            IAssembliesExportedTypesAccessor assembliesExportedTypesAccessor)
        {
            _loadAllServicesFromAssembliesConventionFactory = loadAllServicesFromAssembliesConventionFactory;
            _assembliesExportedTypesAccessor = assembliesExportedTypesAccessor;
        }

        public override void LoadServices(IServiceCollection services, LoadAllServicesFromAssembliesWithConventionConfiguration configuration)
        {
            var convention =
                _loadAllServicesFromAssembliesConventionFactory.CreateFromConventionName(configuration.Convention);
            var allExportedTypes = _assembliesExportedTypesAccessor.GetExportedTypesFromAllAssembliesNames(configuration.Assemblies).ToList();
            allExportedTypes
                .ForEach(service => RegisterService(service, services, convention, allExportedTypes));
        }

        private void RegisterService(Type service, IServiceCollection services, ILoadAllServicesFromAssembliesConvention convention, IEnumerable<Type> allExportedTypes)
        {
            var typesToRegister = convention.GetAllTypesToRegisterForService(service, allExportedTypes);
            typesToRegister.ToList().ForEach(typeToRegister =>
            {
                services.Add(
                    new ServiceDescriptor(service, typeToRegister, convention.DefaultServiceLifetime));
            });
        }
    }
}