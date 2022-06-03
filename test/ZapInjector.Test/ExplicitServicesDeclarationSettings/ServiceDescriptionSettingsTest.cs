using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ZapInjector.ExplicitServicesDeclarationSettings;
using static ZapInjector.Test.TestUtils;

namespace ZapInjector.Test.ExplicitServicesDeclarationSettings
{
    public class ServiceDescriptionSettingsTest
    {
        [TestCase("Singleton", ServiceLifetime.Singleton)]
        [TestCase("Transient", ServiceLifetime.Transient)]
        [TestCase("Scoped", ServiceLifetime.Scoped)]
        [TestCase(null, ServiceLifetime.Scoped)]
        public void GetServiceLifetime_ReturnsServiceLifetime_IfServiceLifetimeNameIsCorrect(string serviceLifetimeName,
            ServiceLifetime serviceLifetime)
        {
            new ServiceDescriptionSettings
            {
                ServiceLifetime = serviceLifetimeName
            }.GetServiceLifetime().Should().Be(serviceLifetime);
        }
        [Test]
        public void GetServiceLifetime_ThrowsInvalidOperationException_IfServiceLifetimeNameIsNotCorrect()
        {
            Act(() => new ServiceDescriptionSettings
            {
                ServiceLifetime = "invalid"
            }.GetServiceLifetime()).Should().Throw<InvalidOperationException>();
        }
        
    }
}