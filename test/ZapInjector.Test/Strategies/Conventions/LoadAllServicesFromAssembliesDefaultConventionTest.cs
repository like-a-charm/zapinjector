using System;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using ZapInjector.Strategies.Conventions;

namespace ZapInjector.Test.Strategies.Conventions
{
    interface IAbstraction {}
    abstract class AbstractClass: IAbstraction {}
    class SuperClass: AbstractClass {}
    class SubClass: SuperClass {}
    
    public class LoadAllServicesFromAssembliesDefaultConventionTest
    {
        private LoadAllServicesFromAssembliesDefaultConvention _loadAllServicesFromAssembliesDefaultConvention;

        private readonly Type[] allExportedTypes = new[]
        {
            typeof(IAbstraction),
            typeof(AbstractClass),
            typeof(SuperClass),
            typeof(SubClass)
        };
        
        [SetUp]
        public void SetUp()
        {
            _loadAllServicesFromAssembliesDefaultConvention = new LoadAllServicesFromAssembliesDefaultConvention();
        }

        [TestCase(typeof(IAbstraction))]
        [TestCase(typeof(SuperClass))]
        public void GetAllTypesToRegisterForService_ShouldReturnAllConcreteSubclasses_IfTheirBothPresent(Type serviceType)
        {
            var typesToRegister = _loadAllServicesFromAssembliesDefaultConvention.GetAllTypesToRegisterForService(serviceType,
                allExportedTypes).ToList();

            typesToRegister.Count().Should().Be(2);
            typesToRegister.Should().Contain(typeof(SuperClass));
            typesToRegister.Should().Contain(typeof(SubClass));
        }
    }
}