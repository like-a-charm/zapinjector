using ZapInjector.Configurations;

namespace ZapInjector.ExplicitServicesDeclarationSettings
{
    public interface IExplicitServicesDeclarationSettingsVisitor<out TOut, out TContext>
    {
        TContext Context { get; }
        TOut Visit(ParameterSettings parameterSettings);
        TOut Visit(ValueSettings valueSettings);
        TOut Visit(ServiceDescriptionSettings parameterSettings);
        TOut Visit(ReferenceSettings referenceSettings);
        TOut Visit(FactorySettings factorySettings);
    }
}