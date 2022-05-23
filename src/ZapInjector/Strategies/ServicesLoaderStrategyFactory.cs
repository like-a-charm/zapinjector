using System;
using Microsoft.Extensions.DependencyInjection;
using ZapInjector.Strategies.Conventions;

namespace ZapInjector.Strategies
{
    public class ServicesLoaderStrategyFactory : IServicesLoaderStrategyFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ServicesLoaderStrategyFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IServicesLoaderStrategy CreateFromName(string strategyName)
        {
            var strategyToCreate = Type.GetType(strategyName);
            
            if (strategyToCreate == null)
            {
                throw new ArgumentException($"Could not activate any strategy from {strategyName}");
            }
            
            return (IServicesLoaderStrategy) ActivatorUtilities.CreateInstance(_serviceProvider, strategyToCreate) ?? throw new InvalidOperationException();
        }
    }
}