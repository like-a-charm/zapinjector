using FluentValidation;

namespace ZapInjector.ExplicitServicesDeclarationSettings.Validators
{
    public class MethodCallSettingsValidator: AbstractValidator<MethodCallSettings>
    {
        private static MethodCallSettingsValidator? _instance;

        public static MethodCallSettingsValidator Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = new MethodCallSettingsValidator();
                _instance.Initialize();
                return _instance;
            }
        }

        public void Initialize()
        {
            RuleFor(x => x.Parameters).NotNull();
            RuleForEach(x => x.Parameters).SetValidator(ParameterSettingsValidator.Instance);
            RuleFor(x => x.MethodName).NotNull().NotEmpty();
        }
    }
}