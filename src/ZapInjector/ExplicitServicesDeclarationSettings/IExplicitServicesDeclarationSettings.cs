namespace ZapInjector.ExplicitServicesDeclarationSettings
{
    public interface IExplicitServicesDeclarationSettings
    {
        TOut AcceptVisitor<TOut, TContext>(IExplicitServicesDeclarationSettingsVisitor<TOut, TContext> visitor);
    }
}