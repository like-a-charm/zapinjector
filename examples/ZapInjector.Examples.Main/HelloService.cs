using System;
using System.Collections.Generic;
using System.Linq;
using ZapInjector.Examples.Abstractions;

namespace ZapInjector.Examples.Main
{
    public class HelloService
    {
        private readonly IEnumerable<INameAbstraction> _nameAbstractions;

        public HelloService(IEnumerable<INameAbstraction> nameAbstractions)
        {
            _nameAbstractions = nameAbstractions;
        }

        public void SayHelloToEveryone()
        {
            _nameAbstractions.ToList().ForEach(nameAbstraction => Console.WriteLine($"Hello {nameAbstraction.Name}!"));
        }
    }
}