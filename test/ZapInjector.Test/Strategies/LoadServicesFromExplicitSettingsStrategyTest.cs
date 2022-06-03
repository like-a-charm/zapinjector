using System.Linq;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using ZapInjector.Configurations;
using ZapInjector.ExplicitServicesDeclarationSettings;
using ZapInjector.ExplicitServicesDeclarationSettings.Validators;
using ZapInjector.Strategies;
using static ZapInjector.Test.TestUtils;

namespace ZapInjector.Test.Strategies
{
    public class LoadServicesFromExplicitSettingsStrategyTest
    {
        private LoadServicesFromExplicitSettingsStrategy _loadServicesFromExplicitSettingsStrategy;
        private IExplicitServiceDeclarationSettingsVisitorFactory _explicitServiceDeclarationSettingsVisitorFactory;
        private ValueSettingsValidator _valueSettingsValidator;
        private ServiceDescriptionSettingsValidator _serviceDescriptionSettingsValidator;

        [SetUp]
        public void SetUp()
        {
            _explicitServiceDeclarationSettingsVisitorFactory = Substitute.For<IExplicitServiceDeclarationSettingsVisitorFactory>();
            _valueSettingsValidator = Substitute.For<ValueSettingsValidator>();
            _serviceDescriptionSettingsValidator = Substitute.For<ServiceDescriptionSettingsValidator>();
            _loadServicesFromExplicitSettingsStrategy = new LoadServicesFromExplicitSettingsStrategy(
                _explicitServiceDeclarationSettingsVisitorFactory,
                _valueSettingsValidator,
                _serviceDescriptionSettingsValidator);
        }

        [Test]
        public void LoadServices_ShouldNotThrowException()
        {
            var visitor = Substitute
                .For<IExplicitServicesDeclarationSettingsVisitor<ServiceFactory, IComputeServiceFactoriesContext>>();
            var serviceFactory = new ServiceFactory
            {
                Type = typeof(object),
                ObjectFactory = (sp, args) => new object(),
                DependenciesFactories = Enumerable.Empty<ServiceFactory>()
            };
            visitor.Visit(Arg.Any<ServiceDescriptionSettings>()).Returns(serviceFactory);
            visitor.Visit(Arg.Any<ValueSettings>()).Returns(serviceFactory);
            
            _explicitServiceDeclarationSettingsVisitorFactory.CreateComputeServiceFactoriesVisitor().Returns(visitor);

            var validationResult = new ValidationResult();
            _serviceDescriptionSettingsValidator.Validate(Arg.Any<ValidationContext<ServiceDescriptionSettings>>())
                .Returns(validationResult);
            _valueSettingsValidator.Validate(Arg.Any<ValidationContext<ValueSettings>>()).Returns(validationResult);

            var configuration = new LoadServicesFromExplicitSettingsConfiguration();
            configuration.Values = new[]
            {
                new ValueSettings
                {
                    Name = "valueName",
                    Type = "ValueType",
                    Value = "aValue"
                }
            };

            configuration.ServiceDescriptions = new[]
            {
                new ServiceDescriptionSettings
                {
                    ServiceName = "serviceName"
                }
            };
            
            Act(() => _loadServicesFromExplicitSettingsStrategy.LoadServices(new ServiceCollection(), configuration))
                .Should().NotThrow();
        }
    }
}