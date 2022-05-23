using System.Collections.Generic;

namespace ZapInjector.Configurations
{
    public interface IServicesLoaderConfiguration
    {
        public IEnumerable<string> Assemblies { get; set; }
    }
}