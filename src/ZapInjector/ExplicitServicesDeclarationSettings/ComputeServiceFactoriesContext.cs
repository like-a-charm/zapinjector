using System;
using System.Collections;
using System.Collections.Generic;

namespace ZapInjector.ExplicitServicesDeclarationSettings
{
    public class ComputeServiceFactoriesContext: IComputeServiceFactoriesContext
    {
        private readonly IDictionary<string, ServiceFactory> _namedServices;

        public ComputeServiceFactoriesContext(IDictionary<string, ServiceFactory> namedServices)
        {
            _namedServices = namedServices;
        }

        public void AddNamedServiceFactory(string name, ServiceFactory serviceFactory)
        {
            if (_namedServices.ContainsKey(name))
            {
                throw new InvalidOperationException(
                    $"A service or parameter named {name} already exists");
            }

            _namedServices.Add(name, serviceFactory);
        }

        public ServiceFactory GetNamedServiceFactory(string name) => _namedServices[name];
    }
}