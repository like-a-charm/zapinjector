using FluentValidation;

namespace ZapInjector.ExplicitServicesDeclarationSettings.Validators
{
    public class ServiceDescriptionSettingsValidator: AbstractValidator<ServiceDescriptionSettings>
    {
        private static ServiceDescriptionSettingsValidator? _instance;

        public static ServiceDescriptionSettingsValidator Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = new ServiceDescriptionSettingsValidator();
                _instance.Initialize();
                return _instance;
            }
        }

        public void Initialize()
        {
            RuleFor(x => x.ServiceType).NotNull().NotEmpty();
            RuleFor(x => x.ImplementationType).Null().When(x => x.ImplementationFactory != null);
            RuleFor(x => x.ImplementationFactory).Null().When(x => x.ImplementationType != null);
            RuleFor(x => x.ImplementationType).NotNull().NotEmpty().When(x => x.ImplementationFactory == null);
            RuleFor(x => x.ImplementationFactory)
                .NotNull()
                .SetValidator(FactorySettingsValidator.Instance)
                .When(x => x.ImplementationType == null);
            RuleForEach(x => x.OnAfterCreate)
                .NotNull()
                .SetValidator(MethodCallSettingsValidator.Instance)
                .When(x => x.OnAfterCreate != null);
            RuleForEach(x => x.Dependencies)
                .NotNull()
                .SetValidator(ParameterSettingsValidator.Instance)
                .When(x => x.Dependencies != null);
        }
    }
}