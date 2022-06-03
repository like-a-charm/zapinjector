using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using ZapInjector.ExplicitServicesDeclarationSettings;
using static ZapInjector.Test.TestUtils;

namespace ZapInjector.Test.ExplicitServicesDeclarationSettings.ComputeServiceFactoriesVisitorTest
{
    public class VisitParametersSettingsTest
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
        public void VisitParameterSettings_ShouldInvokeVisitValueSetting_IfValueIsNotNull()
        {
            var valueSettings = new ValueSettings();
            var expectedResult = new ServiceFactory();
            var parameterSettings = new ParameterSettings
            {
                Value = valueSettings
            };
            _computeServiceFactoriesVisitor.Visit(Arg.Any<ValueSettings>()).Returns(expectedResult);
            _computeServiceFactoriesVisitor.ClearReceivedCalls();
            
            var actualResult = _computeServiceFactoriesVisitor.Visit(parameterSettings);

            _computeServiceFactoriesVisitor.Received(1).Visit(valueSettings);
            actualResult.Should().Be(expectedResult);
        }
        
        [Test]
        public void VisitParameterSettings_ShouldInvokeVisitReferenceSetting_IfReferenceIsNotNull()
        {
            var referenceSettings = new ReferenceSettings();
            var expectedResult = new ServiceFactory();
            var parameterSettings = new ParameterSettings
            {
                Reference = referenceSettings
            };
            _computeServiceFactoriesVisitor.Visit(Arg.Any<ReferenceSettings>()).Returns(expectedResult);
            _computeServiceFactoriesVisitor.ClearReceivedCalls();
            
            var actualResult = _computeServiceFactoriesVisitor.Visit(parameterSettings);

            _computeServiceFactoriesVisitor.Received(1).Visit(referenceSettings);
            actualResult.Should().Be(expectedResult);
        }
        [Test]
        public void VisitServiceDescriptionSettings_ShouldInvokeVisitServiceDescriptionSettings_IfServiceDescriptionIsNotNull()
        {
            var serviceDescription = new ServiceDescriptionSettings();
            var expectedResult = new ServiceFactory();
            var parameterSettings = new ParameterSettings
            {
                ServiceDescription = serviceDescription
            };
            _computeServiceFactoriesVisitor.Visit(Arg.Any<ServiceDescriptionSettings>()).Returns(expectedResult);
            _computeServiceFactoriesVisitor.ClearReceivedCalls();
            
            var actualResult = _computeServiceFactoriesVisitor.Visit(parameterSettings);

            _computeServiceFactoriesVisitor.Received(1).Visit(serviceDescription);
            actualResult.Should().Be(expectedResult);
        }
        [Test]
        public void VisitServiceDescriptionSettings_ShouldThrowInvalidOperationException_IfAllPropertiesOfParameterSettingsAreNull()
        {
            var parameterSettings = new ParameterSettings();
            _computeServiceFactoriesVisitor.ClearReceivedCalls();
            
            Act(() => _computeServiceFactoriesVisitor.Visit(parameterSettings))
                .Should().Throw<InvalidOperationException>();

        }
    }
}