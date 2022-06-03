using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using ZapInjector.Configurations;
using ZapInjector.Strategies;
using static ZapInjector.Test.TestUtils;

namespace ZapInjector.Test.Strategies
{
    public class ServiceLoaderStrategyBaseTest
    {
        private ServicesLoaderStrategyBase<IServicesLoaderConfiguration> _servicesLoaderStrategyBase;

        [SetUp]
        public void SetUp()
        {
            _servicesLoaderStrategyBase = Substitute.For<ServicesLoaderStrategyBase<IServicesLoaderConfiguration>>();
        }

        [Test]
        public void ConfigurationType_ShouldBeIServiceLoaderConfiguration()
        {
            _servicesLoaderStrategyBase.ConfigurationType
                .Should().Be(typeof(IServicesLoaderConfiguration));
        }
        
        [Test]
        public void ConcreteLoadServices_ShouldInvokeAbstractLoadServices_IfConfigurationIsOfTypeIServiceLoaderConfiguration()
        {
            IServiceCollection services = Substitute.For<IServiceCollection>();
            object configuration = Substitute.For<IServicesLoaderConfiguration>();
            Act(() => _servicesLoaderStrategyBase.LoadServices(services, configuration))
                .Should().NotThrow();
            _servicesLoaderStrategyBase.Received(1).LoadServices(Arg.Any<IServiceCollection>(), Arg.Any<IServicesLoaderConfiguration>());
        }
        
        [Test]
        public void ConcreteLoadServices_ShouldThrowArgumentNullException_IfConfigurationIsNull()
        {
            IServiceCollection services = Substitute.For<IServiceCollection>();
            object? configuration = null;
            Act(() => _servicesLoaderStrategyBase.LoadServices(services, configuration))
                .Should().ThrowExactly<ArgumentNullException>();
        }
        
        [Test]
        public void ConcreteLoadServices_ShouldThrowArgumentException_IfConfigurationIsNotOfTypeIServiceLoaderException()
        {
            IServiceCollection services = Substitute.For<IServiceCollection>();
            object configuration = new object();
            Act(() => _servicesLoaderStrategyBase.LoadServices(services, configuration))
                .Should().ThrowExactly<ArgumentException>();
        }
    }
}