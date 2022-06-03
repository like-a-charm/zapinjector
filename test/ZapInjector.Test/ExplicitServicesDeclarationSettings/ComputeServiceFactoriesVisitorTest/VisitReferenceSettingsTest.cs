using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using ZapInjector.ExplicitServicesDeclarationSettings;

namespace ZapInjector.Test.ExplicitServicesDeclarationSettings.ComputeServiceFactoriesVisitorTest
{
    public class VisitReferenceSettingsTest
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
        public void VisitReferenceSettings_ReturnNamedService()
        {
            const string name = nameof(name);
            var expectedResult = new ServiceFactory();
            _computeServiceFactoriesContext.GetNamedServiceFactory(name).Returns(expectedResult);

            var referenceSettings = new ReferenceSettings
            {
                Name = name
            };

            _computeServiceFactoriesVisitor.Visit(referenceSettings)
                .Should().Be(expectedResult);
        }
    }
}