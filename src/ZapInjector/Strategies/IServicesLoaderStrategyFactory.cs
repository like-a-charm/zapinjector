using System;

namespace ZapInjector.Strategies
{
    public interface IServicesLoaderStrategyFactory
    {
        IServicesLoaderStrategy CreateFromName(string strategyName);
    }
}