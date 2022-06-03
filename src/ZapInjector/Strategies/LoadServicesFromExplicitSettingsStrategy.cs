using System.Linq;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ZapInjector.Configurations;
using ZapInjector.ExplicitServicesDeclarationSettings;
using ZapInjector.ExplicitServicesDeclarationSettings.Validators;

namespace ZapInjector.Strategies
{
    public class LoadServicesFromExplicitSettingsStrategy: ServicesLoaderStrategyBase<LoadServicesFromExplicitSettingsConfiguration>
    {


        private readonly IExplicitServiceDeclarationSettingsVisitorFactory
            _explicitServiceDeclarationSettingsVisitorFactory;

        private readonly ValueSettingsValidator _valueSettingsValidator;
        private readonly ServiceDescriptionSettingsValidator _serviceDescriptionSettingsValidator;

        public LoadServicesFromExplicitSettingsStrategy(IExplicitServiceDeclarationSettingsVisitorFactory explicitServiceDeclarationSettingsVisitorFactory, ValueSettingsValidator valueSettingsValidator, ServiceDescriptionSettingsValidator serviceDescriptionSettingsValidator)
        {
            _explicitServiceDeclarationSettingsVisitorFactory = explicitServiceDeclarationSettingsVisitorFactory;
            _valueSettingsValidator = valueSettingsValidator;
            _serviceDescriptionSettingsValidator = serviceDescriptionSettingsValidator;
        }

        public override void LoadServices(IServiceCollection services, LoadServicesFromExplicitSettingsConfiguration configuration)
        {
            var visitor = _explicitServiceDeclarationSettingsVisitorFactory.CreateComputeServiceFactoriesVisitor();

            configuration.Values?.ToList().ForEach(x => _valueSettingsValidator.ValidateAndThrow(x));
            configuration.ServiceDescriptions?.ToList().ForEach(x => _serviceDescriptionSettingsValidator.ValidateAndThrow(x));
            
            configuration.Values?.ToList().ForEach(x => x.AcceptVisitor(visitor));

            configuration.ServiceDescriptions?.ToList().ForEach(serviceDescription =>
            {
                var serviceFactory = serviceDescription.AcceptVisitor(visitor);
                services.Add(serviceFactory.ToServiceDescriptor(serviceDescription.GetServiceLifetime()));
            });
        }
    }
}