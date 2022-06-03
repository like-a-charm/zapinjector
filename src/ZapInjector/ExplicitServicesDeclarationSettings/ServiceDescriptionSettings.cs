using System.Collections.Generic;
using System.Linq;
using ZapInjector.Configurations;

namespace ZapInjector.ExplicitServicesDeclarationSettings
{
    public class ServiceDescriptionSettings: IExplicitServicesDeclarationSettings
    {
        public string? ServiceName { get; set; }
        public string? ServiceType { get; set; }
        public string? ImplementationType { get; set; }
        public FactorySettings? ImplementationFactory { get; set; }
        public IEnumerable<ParameterSettings>? Dependencies { get; set; }
        public IEnumerable<MethodCallSettings>? OnAfterCreate { get; set; }
        public string? ServiceLifetime { get; set; }
        public TOut AcceptVisitor<TOut, TContext>(IExplicitServicesDeclarationSettingsVisitor<TOut, TContext> visitor) => visitor.Visit(this);
    }
}