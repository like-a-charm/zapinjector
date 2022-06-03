using System;
using FluentAssertions;
using NUnit.Framework;
using ZapInjector.ExplicitServicesDeclarationSettings;

namespace ZapInjector.Test.ExplicitServicesDeclarationSettings
{
    public class ValueSettingsExtensionsTest
    {
        [Test]
        public void GetValueObject_ReturnsObject_IfEnvRegexIsNotMatched()
        {
            const string value = nameof(value);
            new ValueSettings
            {
                Type = "System.String",
                Value = value
            }.GetValueObject().Should().Be(value);
        }
        
        [Test]
        public void GetValueObject_ReturnsEnvironmentVariable_IfEnvRegexIsMatched()
        {
            const string envVarName = nameof(envVarName);
            const string value = nameof(value);

            Environment.SetEnvironmentVariable(envVarName, value);
            
            new ValueSettings
            {
                Type = "System.String",
                Value = $"env({envVarName})"
            }.GetValueObject().Should().Be(value);
        }
    }
}