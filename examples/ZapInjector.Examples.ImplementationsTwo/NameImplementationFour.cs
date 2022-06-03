using ZapInjector.Examples.Abstractions;

namespace ZapInjector.Examples.ImplementationsTwo
{
    public class NameImplementationFour: INameAbstraction
    {
        public NameImplementationFour(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public void ChangeName(string newName)
        {
            Name = newName;
        }

        public static NameImplementationFour Create(string name)
        {
            return new NameImplementationFour(name);
        }
    }
}