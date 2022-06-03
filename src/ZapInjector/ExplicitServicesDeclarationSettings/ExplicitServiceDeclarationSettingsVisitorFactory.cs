using System.Collections.Generic;

namespace ZapInjector.ExplicitServicesDeclarationSettings
{
    public class ExplicitServiceDeclarationSettingsVisitorFactory: IExplicitServiceDeclarationSettingsVisitorFactory
    {
        public IExplicitServicesDeclarationSettingsVisitor<ServiceFactory, IComputeServiceFactoriesContext> CreateComputeServiceFactoriesVisitor()
        {
            return new ComputeServiceFactoriesVisitor(new ComputeServiceFactoriesContext(new Dictionary<string, ServiceFactory>()));
        }
    }
}