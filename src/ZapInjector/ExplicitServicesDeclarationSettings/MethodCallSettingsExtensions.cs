using System;
using System.Linq;

namespace ZapInjector.ExplicitServicesDeclarationSettings
{
    public static class MethodCallSettingsExtensions
    {
        public static object? Invoke(
            this MethodCallSettings methodCallSettings,
            Type methodDeclaratorType,
            object? instance,
            IExplicitServicesDeclarationSettingsVisitor<ServiceFactory, IComputeServiceFactoriesContext> visitor,
            IServiceProvider serviceProvider)
        {
            var method = methodDeclaratorType.GetMethod(methodCallSettings.MethodName)!;
            if (instance != null)
            {
                method = instance.GetType().GetMethod(methodCallSettings.MethodName)!;
            }

            var parameters = methodCallSettings.Parameters?
                .Select(x => x.AcceptVisitor(visitor))
                .Select(x => x.CreateInstance(serviceProvider))
                .ToArray();
            
            return method.Invoke(instance, parameters);
        }
    }
}