namespace ZapInjector.ExplicitServicesDeclarationSettings
{
    public interface IComputeServiceFactoriesContext
    {
        void AddNamedServiceFactory(string name, ServiceFactory serviceFactory);
        ServiceFactory GetNamedServiceFactory(string name);
    }
}