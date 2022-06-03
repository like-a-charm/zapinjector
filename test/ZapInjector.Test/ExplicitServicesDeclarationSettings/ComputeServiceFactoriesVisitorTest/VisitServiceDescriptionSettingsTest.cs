using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using ZapInjector.ExplicitServicesDeclarationSettings;

namespace ZapInjector.Test.ExplicitServicesDeclarationSettings.ComputeServiceFactoriesVisitorTest
{
    interface IServiceType {}

    class ImplementationType : IServiceType
    {
        public bool HasMethodBeenCalled { get; private set; }

        public void AMethod()
        {
            HasMethodBeenCalled = true;
        }
    }

    class ImplementationWithDependency: IServiceType
    {
        private readonly string _dependency;

        public ImplementationWithDependency(string dependency)
        {
            _dependency = dependency;
        }
    }
    
    public class VisitServiceDescriptionSettingsTest
    {
        private ComputeServiceFactoriesVisitor _computeServiceFactoriesVisitor;
        private IComputeServiceFactoriesContext _computeServiceFactoriesContext;

        [SetUp]
        public void SetUp()
        {
            _computeServiceFactoriesContext = Substitute.For<IComputeServiceFactoriesContext>();
            _computeServiceFactoriesVisitor = Substitute.ForPartsOf<ComputeServiceFactoriesVisitor>(_computeServiceFactoriesContext);
        }

        [Test]
        public void
            VisitServiceDescriptionSettings_ReturnsServiceFactoryForImplementationType_IfImplementationTypeIsNotNull()
        {
            var serviceDescription = new ServiceDescriptionSettings
            {
                Dependencies = Enumerable.Empty<ParameterSettings>(),
                ImplementationType = $"{typeof(ImplementationType).FullName}, {typeof(ImplementationType).Assembly.GetName().Name}",
                ServiceLifetime = nameof(ServiceLifetime.Singleton),
                ServiceType = $"{typeof(IServiceType).FullName}, {typeof(ImplementationType).Assembly.GetName().Name}"
            };

            var actualResult = _computeServiceFactoriesVisitor.Visit(serviceDescription);

            actualResult.Should().NotBeNull();
            actualResult.ObjectFactory!.Invoke(null, null)
                .Should()
                .BeOfType(typeof(ImplementationType));
        }
        
        [Test]
        public void
            VisitServiceDescriptionSettings_ReturnsServiceFactoryForImplementationFactory_IfImplementationFactoryIsNotNull()
        {
            var factorySettings = new FactorySettings();
            var serviceDescription = new ServiceDescriptionSettings
            {
                Dependencies = Enumerable.Empty<ParameterSettings>(),
                ImplementationFactory = factorySettings,
                ServiceLifetime = nameof(ServiceLifetime.Singleton),
                ServiceType = $"{typeof(IServiceType).FullName}, {typeof(ImplementationType).Assembly.GetName().Name}"
            };

            var expectedResult = new ServiceFactory();
            _computeServiceFactoriesVisitor.Visit(Arg.Any<FactorySettings>()).Returns(expectedResult);
            _computeServiceFactoriesVisitor.ClearReceivedCalls();

            var actualResult = _computeServiceFactoriesVisitor.Visit(serviceDescription);

            actualResult.Should().Be(expectedResult);
            _computeServiceFactoriesVisitor.Received(1).Visit(factorySettings);
        }
        
        [Test]
        public void
            VisitServiceDescriptionSettings_ReturnsServiceFactoryForImplementationTypeWithDependencies_IfDependenciesAreProvided()
        {
            const string dependency = "a dependency";
            var parameterSettings = new ParameterSettings();

            var dependencyFactory = new ServiceFactory()
            {
                Type = typeof(string),
                ObjectFactory = (sp, args) => dependency
            };
            
            var serviceDescription = new ServiceDescriptionSettings
            {
                Dependencies = new [] { parameterSettings },
                ImplementationType = $"{typeof(ImplementationWithDependency).FullName}, {typeof(ImplementationWithDependency).Assembly.GetName().Name}",
                ServiceLifetime = nameof(ServiceLifetime.Singleton),
                ServiceType = $"{typeof(IServiceType).FullName}, {typeof(ImplementationType).Assembly.GetName().Name}"
            };

            _computeServiceFactoriesVisitor.Visit(Arg.Any<ParameterSettings>()).Returns(dependencyFactory);
            var actualResult = _computeServiceFactoriesVisitor.Visit(serviceDescription);

            actualResult.Should().NotBeNull();
            actualResult.DependenciesFactories.Should().NotBeEmpty();
            actualResult.DependenciesFactories!.Count().Should().Be(1);
            actualResult.DependenciesFactories!.First().ObjectFactory!.Should().Be(dependencyFactory.ObjectFactory);
            actualResult.CreateInstance(null)
                .Should()
                .BeOfType(typeof(ImplementationWithDependency));
        }
        
        [Test]
        public void
            VisitServiceDescriptionSettings_CallsOnAfterCreatMethods_IfOnAfterCreatMethodsAreProvided()
        {
            var serviceDescription = new ServiceDescriptionSettings
            {
                Dependencies = Enumerable.Empty<ParameterSettings>(),
                ImplementationType = $"{typeof(ImplementationType).FullName}, {typeof(ImplementationType).Assembly.GetName().Name}",
                ServiceLifetime = nameof(ServiceLifetime.Singleton),
                ServiceType = $"{typeof(IServiceType).FullName}, {typeof(ImplementationType).Assembly.GetName().Name}",
                OnAfterCreate = new[]
                {
                    new MethodCallSettings
                    {
                        MethodName = "AMethod"
                    }
                }
            };

            var actualResult = _computeServiceFactoriesVisitor.Visit(serviceDescription);

            actualResult.Should().NotBeNull();
            var implementation = actualResult.ObjectFactory!.Invoke(null, null);
            implementation.Should()
                .BeOfType(typeof(ImplementationType));
            (implementation as ImplementationType)!.HasMethodBeenCalled.Should().BeTrue();
        }

        [Test]
        public void
            VisitServiceDescriptionSettings_ShouldAddNamedServiceFactory_IfNameIsNotNull()
        {
            const string serviceName = nameof(serviceName);
            var serviceDescription = new ServiceDescriptionSettings
            {
                Dependencies = Enumerable.Empty<ParameterSettings>(),
                ImplementationType = $"{typeof(ImplementationType).FullName}, {typeof(ImplementationType).Assembly.GetName().Name}",
                ServiceLifetime = nameof(ServiceLifetime.Singleton),
                ServiceType = $"{typeof(IServiceType).FullName}, {typeof(ImplementationType).Assembly.GetName().Name}",
                ServiceName = serviceName
            };
            

            var actualResult = _computeServiceFactoriesVisitor.Visit(serviceDescription);

            actualResult.Should().NotBeNull();
            actualResult.ObjectFactory!.Invoke(null, null)
                .Should()
                .BeOfType(typeof(ImplementationType));
            
            _computeServiceFactoriesContext.Received(1).AddNamedServiceFactory(serviceName, Arg.Any<ServiceFactory>());
        }

    }
}