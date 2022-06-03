using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using ZapInjector.Strategies.Conventions;
using static ZapInjector.Test.TestUtils;

namespace ZapInjector.Test.Strategies.Conventions
{
    class TestConvention: ILoadAllServicesFromAssembliesConvention
    {
        public IEnumerable<Type> GetAllTypesToRegisterForService(Type service, IEnumerable<Type> allTypes)
        {
            throw new NotImplementedException();
        }

        public ServiceLifetime DefaultServiceLifetime { get; }
    }
    
    public class LoadAllServicesFromAssembliesConventionFactoryTest
    {
        private LoadAllServicesFromAssembliesConventionFactory _loadAllServicesFromAssembliesConventionFactory;
        private IServiceProvider _serviceProvider;

        [SetUp]
        public void SetUp()
        {
            _serviceProvider = Substitute.For<IServiceProvider>();
            _loadAllServicesFromAssembliesConventionFactory = new LoadAllServicesFromAssembliesConventionFactory(_serviceProvider);
        }

        [Test]
        public void CreateFromName_ShouldCreateConvention_IfConventionExists()
        {
            string conventionName =
                $"{typeof(TestConvention).FullName}, {typeof(TestConvention).Assembly.GetName().Name}";

            var convention = _loadAllServicesFromAssembliesConventionFactory.CreateFromConventionName(conventionName);

            convention.Should().NotBeNull();
            convention.Should().BeOfType(typeof(TestConvention));
        }

        [Test]
        public void CreateFromName_ShouldThrowArgumentException_IfConventionDoesntExist()
        {
            string conventionName = "an invalid convention name";
            Act(() => _loadAllServicesFromAssembliesConventionFactory.CreateFromConventionName(conventionName))
                .Should().Throw<ArgumentException>();
        }
    }
}