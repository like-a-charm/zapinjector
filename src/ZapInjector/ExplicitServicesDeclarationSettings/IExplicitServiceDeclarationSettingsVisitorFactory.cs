namespace ZapInjector.ExplicitServicesDeclarationSettings
{
    public interface IExplicitServiceDeclarationSettingsVisitorFactory
    {
        IExplicitServicesDeclarationSettingsVisitor<ServiceFactory, IComputeServiceFactoriesContext> CreateComputeServiceFactoriesVisitor();
    }
}