using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using ZapInjector.ExplicitServicesDeclarationSettings;

namespace ZapInjector.Test.ExplicitServicesDeclarationSettings.ComputeServiceFactoriesVisitorTest
{
    public class VisitValueSettingsTest
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
        public void VisitValueSettings_ShouldReturnServiceFactory()
        {
            const string value = nameof(value);
            var valueSettings = new ValueSettings
            {
                Value = value,
                Type = "System.String"
            };

            var serviceFactory = _computeServiceFactoriesVisitor.Visit(valueSettings);

            serviceFactory.Should().NotBeNull();
            serviceFactory.ObjectFactory!.Invoke(null, null).Should().Be(value);
        }
        
        [Test]
        public void VisitValueSettings_ShouldAddNamedService_IfValueSettingsNameIsProvided()
        {
            const string value = nameof(value);
            const string name = nameof(name);
            var valueSettings = new ValueSettings
            {
                Value = value,
                Type = "System.String",
                Name = name
            };

            var serviceFactory = _computeServiceFactoriesVisitor.Visit(valueSettings);

            _computeServiceFactoriesContext.Received(1).AddNamedServiceFactory(name, Arg.Any<ServiceFactory>());
            
            serviceFactory.Should().NotBeNull();
            serviceFactory.ObjectFactory!.Invoke(null, null).Should().Be(value);
            
        }
    }
}