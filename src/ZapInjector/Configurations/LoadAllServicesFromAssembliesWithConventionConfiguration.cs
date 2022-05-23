using System.Collections.Generic;
using System.Linq;

namespace ZapInjector.Configurations
{
    public class LoadAllServicesFromAssembliesWithConventionConfiguration : IServicesLoaderConfiguration
    {
        public IEnumerable<string> Assemblies { get; set; }
        public string Convention { get; set; }
    }
}