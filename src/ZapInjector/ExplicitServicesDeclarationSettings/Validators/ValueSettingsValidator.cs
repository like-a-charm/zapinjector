using FluentValidation;

namespace ZapInjector.ExplicitServicesDeclarationSettings.Validators
{
    public class ValueSettingsValidator: AbstractValidator<ValueSettings>
    {
        private static ValueSettingsValidator? _instance;

        public static ValueSettingsValidator Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = new ValueSettingsValidator();
                _instance.Initialize();
                return _instance;
            }
        }

        public void Initialize()
        {
            RuleFor(x => x.Type).NotNull().NotEmpty();
            RuleFor(x => x.Type).NotNull();
        }
    }
}