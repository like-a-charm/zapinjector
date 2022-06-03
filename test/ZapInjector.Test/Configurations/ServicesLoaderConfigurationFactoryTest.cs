using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using ZapInjector.Configurations;

namespace ZapInjector.Test.Configurations
{
    class TestConfiguration : IServicesLoaderConfiguration
    {
        public IEnumerable<string> Assemblies { get; set; }
    }

    public class ServicesLoaderConfigurationFactoryTest
    {
        private ServicesLoaderConfigurationFactory _servicesLoaderConfigurationFactory;
        private IServiceProvider _serviceProvider;

        [SetUp]
        public void SetUp()
        {
            _serviceProvider = Substitute.For<IServiceProvider>();
            _servicesLoaderConfigurationFactory = new ServicesLoaderConfigurationFactory(_serviceProvider);
        }

        [Test]
        public void CreateFromType_ShouldCreateConfiguration()
        {
            IConfiguration configurationRoot = Substitute.For<IConfiguration>();
            var configuration =
                _servicesLoaderConfigurationFactory.CreateFromType(typeof(TestConfiguration), configurationRoot);
            configuration.Should().NotBeNull();
            configuration.Should().BeOfType(typeof(TestConfiguration));
        }
    }
}