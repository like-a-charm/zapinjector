using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using ZapInjector.Strategies;
using static ZapInjector.Test.TestUtils;

namespace ZapInjector.Test.Strategies
{
    class TestServicesLoaderStrategy: IServicesLoaderStrategy
    {
        public Type ConfigurationType { get; }
        public void LoadServices(IServiceCollection services, object configuration)
        {
            throw new NotImplementedException();
        }
    }
    
    public class ServicesLoaderStrategyFactoryTest
    {
        private ServicesLoaderStrategyFactory _servicesLoaderStrategyFactory;
        private IServiceProvider _serviceProvider;

        [SetUp]
        public void SetUp()
        {
            _serviceProvider = Substitute.For<IServiceProvider>();
            _servicesLoaderStrategyFactory = new ServicesLoaderStrategyFactory(_serviceProvider);
        }

        [Test]
        public void CreateFromName_ShouldCreateStrategy_IfStrategyExists()
        {
            string strategyName = $"{typeof(TestServicesLoaderStrategy).FullName}, {typeof(TestServicesLoaderStrategy).Assembly.GetName().Name}";

            var strategy = _servicesLoaderStrategyFactory.CreateFromName(strategyName);

            strategy.Should().NotBeNull();
            strategy.Should().BeOfType(typeof(TestServicesLoaderStrategy));
        }
        
        [Test]
        public void CreateFromName_ShouldThrowArgumentException_IfStrategyDoesntExist()
        {
            string strategyName = "an invalid strategy name";
            Act(() => _servicesLoaderStrategyFactory.CreateFromName(strategyName)).Should().Throw<ArgumentException>();
        }
    }
}