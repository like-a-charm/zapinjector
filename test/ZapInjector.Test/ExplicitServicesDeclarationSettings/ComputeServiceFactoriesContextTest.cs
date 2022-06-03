using System;
using static ZapInjector.Test.TestUtils;
using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using ZapInjector.ExplicitServicesDeclarationSettings;

namespace ZapInjector.Test.ExplicitServicesDeclarationSettings
{
    public class ComputeServiceFactoriesContextTest
    {
        private ComputeServiceFactoriesContext _computeServiceFactoriesContext;
        private IDictionary<string, ServiceFactory> _namedServices;

        [SetUp]
        public void SetUp()
        {
            _namedServices = Substitute.For<IDictionary<string, ServiceFactory>>();
            _computeServiceFactoriesContext = new ComputeServiceFactoriesContext(_namedServices);
        }

        [Test]
        public void AddNamedServiceFactory_ShouldAddNamedService_IfNamedServiceDoesntExist()
        {
            const string serviceName = nameof(serviceName);
            ServiceFactory serviceFactory = new ServiceFactory();

            _namedServices.ContainsKey(serviceName).Returns(false);

            Act(() => _computeServiceFactoriesContext.AddNamedServiceFactory(serviceName, serviceFactory))
                .Should().NotThrow();
            
            _namedServices.Received(1).Add(serviceName, serviceFactory);
        }
        
        [Test]
        public void AddNamedServiceFactory_ShouldThrowInvalidOperationException_IfNamedServiceAlreadyExists()
        {
            const string serviceName = nameof(serviceName);
            ServiceFactory serviceFactory = new ServiceFactory();

            _namedServices.ContainsKey(serviceName).Returns(true);

            Act(() => _computeServiceFactoriesContext.AddNamedServiceFactory(serviceName, serviceFactory))
                .Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void GetNamedServiceFactory_ShouldReturnServiceFactory()
        {
            const string serviceName = nameof(serviceName);
            ServiceFactory serviceFactory = new ServiceFactory();

            _namedServices[serviceName].Returns(serviceFactory);

            _computeServiceFactoriesContext.GetNamedServiceFactory(serviceName)
                .Should().Be(serviceFactory);
        }
    }
}