using FluentAssertions;
using NUnit.Framework;
using ZapInjector.ExplicitServicesDeclarationSettings;

namespace ZapInjector.Test.ExplicitServicesDeclarationSettings
{
    public class ExplicitServiceDeclarationSettingsVisitorFactoryTest
    {
        private ExplicitServiceDeclarationSettingsVisitorFactory _explicitServiceDeclarationSettingsVisitorFactory;

        [SetUp]
        public void SetUp()
        {
            _explicitServiceDeclarationSettingsVisitorFactory = new ExplicitServiceDeclarationSettingsVisitorFactory();
        }

        [Test]
        public void CreateComputeServiceFactoriesVisitor_ShouldReturnComputeServiceFactoriesVisitor()
        {
            var visitor = _explicitServiceDeclarationSettingsVisitorFactory.CreateComputeServiceFactoriesVisitor();
            visitor.Should().NotBeNull();
            visitor.Should().BeOfType<ComputeServiceFactoriesVisitor>();
        }
    }
}