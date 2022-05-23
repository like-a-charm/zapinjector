using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using ZapInjector.Configurations;
using ZapInjector.Strategies;
using ZapInjector.Strategies.Conventions;
using ZapInjector.Utils;

namespace ZapInjector.Test.Strategies
{
    class TestType
    {
        
    }
    
    public class LoadAllServicesFromAssembliesWithConventionStrategyTest
    {
        private LoadAllServicesFromAssembliesWithConventionStrategy
            _loadAllServicesFromAssembliesWithConventionStrategy;

        private IAssembliesExportedTypesAccessor _assembliesExportedTypesAccessor;
        private ILoadAllServicesFromAssembliesConventionFactory _loadAllServicesFromAssembliesConventionFactory;

        [SetUp]
        public void SetUp()
        {
            _loadAllServicesFromAssembliesConventionFactory =
                Substitute.For<ILoadAllServicesFromAssembliesConventionFactory>();
            _assembliesExportedTypesAccessor = Substitute.For<IAssembliesExportedTypesAccessor>();
            _loadAllServicesFromAssembliesWithConventionStrategy =
                new LoadAllServicesFromAssembliesWithConventionStrategy(_loadAllServicesFromAssembliesConventionFactory,
                    _assembliesExportedTypesAccessor);
        }

        [Test]
        public void LoadServices()
        {
            const string conventionName = "conventionName";
            const string assemblyName = "assemblyName";
            Type testType = typeof(TestType);
            Type[] testTypes = { testType };
            string[] assemblies = { assemblyName };
            
            IServiceCollection services = Substitute.For<IServiceCollection>();
            LoadAllServicesFromAssembliesWithConventionConfiguration configuration =
                new();
            ILoadAllServicesFromAssembliesConvention convention =
                Substitute.For<ILoadAllServicesFromAssembliesConvention>();

            _assembliesExportedTypesAccessor.GetExportedTypesFromAllAssembliesNames(assemblies).Returns(testTypes);

            convention.GetAllTypesToRegisterForService(Arg.Any<Type>(), Arg.Any<IEnumerable<Type>>()).Returns(testTypes);
            
            configuration.Convention = conventionName;
            configuration.Assemblies = assemblies;
            
            _loadAllServicesFromAssembliesConventionFactory.CreateFromConventionName(conventionName)
                .Returns(convention);
            
            services.When(s => s.Add(Arg.Any<ServiceDescriptor>())).Do((callInfo) => { });
            
            _loadAllServicesFromAssembliesWithConventionStrategy.LoadServices(services, configuration);
            
            services.Received(1).Add(Arg.Any<ServiceDescriptor>());
        }
    }
}