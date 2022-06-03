using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using ZapInjector.Configurations;
using ZapInjector.Strategies;
using FluentAssertions;
using static ZapInjector.Test.TestUtils;

namespace ZapInjector.Test
{
    public class LoadServicesDirectorTest
    {
        private IServicesLoaderStrategyFactory _servicesLoaderStrategyFactory;
        private IServicesLoaderConfigurationFactory _servicesLoaderConfigurationFactory;
        private LoadServicesDirector _loadServicesDirector;

        [SetUp]
        public void SetUp()
        {
            _servicesLoaderConfigurationFactory = Substitute.For<IServicesLoaderConfigurationFactory>();
            _servicesLoaderStrategyFactory = Substitute.For<IServicesLoaderStrategyFactory>();
            _loadServicesDirector =
                new LoadServicesDirector(_servicesLoaderStrategyFactory, _servicesLoaderConfigurationFactory);
        }

        [Test]
        public void LoadServices_ShouldAddServices()
        {
            const string strategyName = "a strategy";

            IConfiguration configurationRoot = Substitute.For<IConfiguration>();
            IServiceCollection services = Substitute.For<IServiceCollection>();

            IConfigurationSection configurationChild = Substitute.For<IConfigurationSection>();
            configurationChild["Strategy"].Returns(strategyName);
            IEnumerable<IConfigurationSection> children = new[] { configurationChild };
            IConfigurationSection zapInjectorSection = Substitute.For<IConfigurationSection>();
            zapInjectorSection.GetChildren().Returns(children);
            configurationRoot.GetSection("ZapInjector").Returns(zapInjectorSection);

            IServicesLoaderConfiguration servicesLoaderConfiguration = Substitute.For<IServicesLoaderConfiguration>();
            _servicesLoaderConfigurationFactory.CreateFromType(typeof(IServicesLoaderConfiguration), configurationChild)
                .Returns(servicesLoaderConfiguration);

            IServicesLoaderStrategy strategy = Substitute.For<IServicesLoaderStrategy>();
            strategy.ConfigurationType.Returns(typeof(IServicesLoaderConfiguration));
            strategy.WhenForAnyArgs(x => x.LoadServices(services, servicesLoaderConfiguration)).Do(callInfo => {  });

            _servicesLoaderStrategyFactory.CreateFromName(strategyName).Returns(strategy);
            
            Act(() => _loadServicesDirector.LoadServices(services, configurationRoot)).Should().NotThrow();

            strategy.Received(1).LoadServices(services, servicesLoaderConfiguration);
        }
    }
}