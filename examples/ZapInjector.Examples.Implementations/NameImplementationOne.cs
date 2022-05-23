using ZapInjector.Examples.Abstractions;

namespace ZapInjector.Examples.Implementations
{
    public class NameImplementationOne : INameAbstraction
    {
        public string Name => "George";
    }
}