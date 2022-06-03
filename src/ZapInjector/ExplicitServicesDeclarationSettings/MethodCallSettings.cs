using System.Collections.Generic;

namespace ZapInjector.ExplicitServicesDeclarationSettings
{
    public class MethodCallSettings
    {
        public string MethodName { get; set; }
        public IEnumerable<ParameterSettings> Parameters { get; set; }

    }
}