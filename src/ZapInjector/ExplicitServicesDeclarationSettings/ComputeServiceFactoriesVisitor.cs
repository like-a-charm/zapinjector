using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace ZapInjector.ExplicitServicesDeclarationSettings
{
    public class ComputeServiceFactoriesVisitor : IExplicitServicesDeclarationSettingsVisitor<ServiceFactory, IComputeServiceFactoriesContext>
    {
        public ComputeServiceFactoriesVisitor(IComputeServiceFactoriesContext context)
        {
            Context = context;
        }

        public IComputeServiceFactoriesContext Context { get; }

        public virtual ServiceFactory Visit(ParameterSettings parameterSettings)
        {
            return parameterSettings.Reference?.AcceptVisitor(this) ??
                   parameterSettings.Value?.AcceptVisitor(this) ??
                   parameterSettings.ServiceDescription?.AcceptVisitor(this) ??
                   throw new InvalidOperationException();
        }

        public virtual ServiceFactory Visit(ValueSettings valueSettings)
        {
            object value = valueSettings.GetValueObject();
            Type valueType = value.GetType();
            var result = new ServiceFactory
            {
                DependenciesFactories = Enumerable.Empty<ServiceFactory>(),
                Type = valueType,
                ObjectFactory = (sp, args) => value
            };
            if (!string.IsNullOrWhiteSpace(valueSettings.Name))
            {
                Context.AddNamedServiceFactory(valueSettings.Name, result);
            }

            return result;

        }

        public virtual ServiceFactory Visit(ServiceDescriptionSettings serviceDescriptionSettings)
        {

            Type serviceType = Type.GetType(serviceDescriptionSettings.ServiceType!)!;
            var result = new ServiceFactory
            {
                Type = serviceType,
                ObjectFactory = null
            };

            if (!string.IsNullOrWhiteSpace(serviceDescriptionSettings.ImplementationType))
            {
                Type implementationType = Type.GetType(serviceDescriptionSettings.ImplementationType!)!;

                IEnumerable<ServiceFactory> dependenciesFactoriesInfo =
                    serviceDescriptionSettings.Dependencies?.Select(x => x.AcceptVisitor(this)) ??
                    Enumerable.Empty<ServiceFactory>();

                var resultDependenciesFactoriesInfo = dependenciesFactoriesInfo as ServiceFactory[] ?? dependenciesFactoriesInfo.ToArray();
                
                result.ObjectFactory = ActivatorUtilities.CreateFactory(implementationType, resultDependenciesFactoriesInfo
                    .Select(x => x.Type!)
                    .ToArray());

                result.DependenciesFactories = resultDependenciesFactoriesInfo;
            }

            if (serviceDescriptionSettings.ImplementationFactory != null)
            {
                result = serviceDescriptionSettings.ImplementationFactory.AcceptVisitor(this);
            }

            if (serviceDescriptionSettings.OnAfterCreate?.Any() ?? false)
            {
                var tmp = result.ObjectFactory;
                result.ObjectFactory = (sp, args) =>
                {
                    var instance = tmp!(sp, args);
                    serviceDescriptionSettings.OnAfterCreate.ToList().ForEach(method =>
                    {
                        method.Invoke(result.Type!, instance, this, sp);
                    });
                    return instance;
                };
            }

            if (!string.IsNullOrWhiteSpace(serviceDescriptionSettings.ServiceName))
            {
                Context.AddNamedServiceFactory(serviceDescriptionSettings.ServiceName, result);
            }

            return result;
        }

        public virtual ServiceFactory Visit(ReferenceSettings referenceSettings) =>
            Context.GetNamedServiceFactory(referenceSettings.Name);

        public virtual ServiceFactory Visit(FactorySettings factorySettings)
        {
            Type? methodDeclaratorType = null;
            ServiceFactory? instanceFactory = null;
            Type factoryInstanceType = Type.GetType(factorySettings.InstanceType!)!;
            
            
            if (!string.IsNullOrWhiteSpace(factorySettings.StaticReference))
            {
                methodDeclaratorType = Type.GetType(factorySettings.StaticReference);
            }

            if (factorySettings.DynamicReference != null)
            {
                var info = factorySettings.DynamicReference.AcceptVisitor(this);
                methodDeclaratorType = info.Type;
                instanceFactory = info;
            }

            return new ServiceFactory
            {
                Type = factoryInstanceType,
                DependenciesFactories = Enumerable.Empty<ServiceFactory>(),
                ObjectFactory = (sp, args) =>
                {
                    object? callerInstance = instanceFactory?.CreateInstance(sp) ?? null;
                    return factorySettings.Method!.Invoke(methodDeclaratorType!, callerInstance!, this, sp)!;
                }
            };

        }
    }
}