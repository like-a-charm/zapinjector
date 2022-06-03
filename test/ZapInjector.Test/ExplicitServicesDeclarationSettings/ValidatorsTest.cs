using FluentAssertions;
using NUnit.Framework;
using ZapInjector.ExplicitServicesDeclarationSettings.Validators;

namespace ZapInjector.Test.ExplicitServicesDeclarationSettings
{
    public class ValidatorsTest
    {
        [Test]
        public void FactorySettingsValidatorInstance_ShouldReturnFactorySettingsValidatorInstance()
        {
            FactorySettingsValidator.Instance.Should().BeOfType<FactorySettingsValidator>();
        }
        [Test]
        public void MethodCallSettingsValidatorInstance_ShouldReturnMethodCallSettingsValidatorInstance()
        {
            MethodCallSettingsValidator.Instance.Should().BeOfType<MethodCallSettingsValidator>();
        }
        [Test]
        public void ParameterSettingsValidatorInstance_ShouldReturnParameterSettingsValidatorInstance()
        {
            ParameterSettingsValidator.Instance.Should().BeOfType<ParameterSettingsValidator>();
        }
        [Test]
        public void ReferenceSettingsValidatorInstance_ShouldReturnReferenceSettingsValidatorInstance()
        {
            ReferenceSettingsValidator.Instance.Should().BeOfType<ReferenceSettingsValidator>();
        }
        [Test]
        public void ServiceDescriptionSettingsValidatorInstance_ShouldReturnServiceDescriptionSettingsValidatorInstance()
        {
            ServiceDescriptionSettingsValidator.Instance.Should().BeOfType<ServiceDescriptionSettingsValidator>();
        }
        [Test]
        public void ValueSettingsValidatorInstance_ShouldReturnValueSettingsValidatorInstance()
        {
            ValueSettingsValidator.Instance.Should().BeOfType<ValueSettingsValidator>();
        }
    }
}