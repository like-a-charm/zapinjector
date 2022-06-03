using System.Collections.Generic;
using ZapInjector.ExplicitServicesDeclarationSettings;

namespace ZapInjector.Configurations
{
    public class LoadServicesFromExplicitSettingsConfiguration: IServicesLoaderConfiguration
    {
        public IEnumerable<ServiceDescriptionSettings>? ServiceDescriptions { get; set; }
        public IEnumerable<ValueSettings>? Values { get; set; }
    }
}