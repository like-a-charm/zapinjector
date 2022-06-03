using FluentValidation;

namespace ZapInjector.ExplicitServicesDeclarationSettings.Validators
{
    public class FactorySettingsValidator: AbstractValidator<FactorySettings>
    {
        private static FactorySettingsValidator? _instance;

        public static FactorySettingsValidator Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = new FactorySettingsValidator();
                _instance.Initialize();
                return _instance;
            }
                
        }

        public void Initialize()
        {
            RuleFor(x => x.InstanceType).NotEmpty().NotNull();
            RuleFor(x => x.Method).NotEmpty().NotNull();
            RuleFor(x => x.StaticReference).NotEmpty().NotNull().When(x => x.DynamicReference == null);
            RuleFor(x => x.StaticReference).Null().When(x => x.DynamicReference != null);
            RuleFor(x => x.DynamicReference).NotNull().SetValidator(ParameterSettingsValidator.Instance).When(x => string.IsNullOrWhiteSpace(x.StaticReference));
            RuleFor(x => x.DynamicReference).Null().When(x => !string.IsNullOrWhiteSpace(x.StaticReference));
            RuleFor(x => x.Method).SetValidator(MethodCallSettingsValidator.Instance).When(x => x.Method != null);
        }
    }
}