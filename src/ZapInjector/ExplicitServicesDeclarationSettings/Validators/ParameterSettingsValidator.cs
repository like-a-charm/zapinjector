using FluentValidation;

namespace ZapInjector.ExplicitServicesDeclarationSettings.Validators
{
    public class ParameterSettingsValidator: AbstractValidator<ParameterSettings>
    {
        private static ParameterSettingsValidator? _instance;

        public static ParameterSettingsValidator Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = new ParameterSettingsValidator();
                _instance.Initialize();
                return _instance;
            }
        }
        
        public void Initialize()
        {
            RuleFor(x => x.Reference).Null().When(x => x.Value != null || x.ServiceDescription != null);
            RuleFor(x => x.Value).Null().When(x => x.Reference != null || x.ServiceDescription != null);
            RuleFor(x => x.ServiceDescription).Null().When(x => x.Value != null || x.Reference != null);
            RuleFor(x => x.Reference)
                .NotNull()
                .SetValidator(ReferenceSettingsValidator.Instance)
                .When(x => x.Value == null && x.ServiceDescription == null);
            RuleFor(x => x.Value)
                .NotNull()
                .SetValidator(ValueSettingsValidator.Instance)
                .When(x => x.Reference == null && x.ServiceDescription == null);
            RuleFor(x => x.ServiceDescription)
                .NotNull()
                .SetValidator(ServiceDescriptionSettingsValidator.Instance)
                .When(x => x.Value == null && x.Reference == null);
        }
    }
}