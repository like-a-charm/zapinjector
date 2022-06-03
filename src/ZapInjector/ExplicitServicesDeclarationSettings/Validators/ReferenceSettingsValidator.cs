using FluentValidation;

namespace ZapInjector.ExplicitServicesDeclarationSettings.Validators
{
    public class ReferenceSettingsValidator: AbstractValidator<ReferenceSettings>
    {
        private static ReferenceSettingsValidator? _instance;

        public static ReferenceSettingsValidator Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = new ReferenceSettingsValidator();
                _instance.Initialize();
                return _instance;
            }
        }
        
        public void Initialize()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}