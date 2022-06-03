using System;
using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ZapInjector.Strategies.Conventions;

namespace ZapInjector.Test.Strategies.Conventions
{
    interface IAbstraction {}
    abstract class AbstractClass: IAbstraction {}
    class SuperClass: AbstractClass {}
    class SubClass: SuperClass {}
    interface IGenericAbstraction<T> {}

    abstract class AbstractGenericClass<T> : IGenericAbstraction<T> {}
    abstract class GenericClass<T>: AbstractGenericClass<T> {}

    public class LoadAllServicesFromAssembliesDefaultConventionTest
    {
        private LoadAllServicesFromAssembliesDefaultConvention _loadAllServicesFromAssembliesDefaultConvention;

        private readonly Type[] _allExportedTypes = new[]
        {
            typeof(IAbstraction),
            typeof(AbstractClass),
            typeof(SuperClass),
            typeof(SubClass),
            typeof(IGenericAbstraction<>),
            typeof(AbstractGenericClass<>),
            typeof(GenericClass<>)
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
                _allExportedTypes).ToList();

            typesToRegister.Count().Should().Be(2);
            typesToRegister.Should().Contain(typeof(SuperClass));
            typesToRegister.Should().Contain(typeof(SubClass));
        }
        [TestCase(typeof(IGenericAbstraction<>))]
        [TestCase(typeof(GenericClass<>))]
        public void GetAllTypesToRegisterForService_ShouldReturnEmptyResult_IfServiceIsAGenericClassOrInterface(Type serviceType)
        {
            var typesToRegister = _loadAllServicesFromAssembliesDefaultConvention.GetAllTypesToRegisterForService(serviceType,
                _allExportedTypes).ToList();

            typesToRegister.Should().BeEmpty();
        }

        [Test]
        public void DefaultServiceLifetime_ShouldBeScoped()
        {
            _loadAllServicesFromAssembliesDefaultConvention.DefaultServiceLifetime.Should().Be(ServiceLifetime.Scoped);
        }
    }
}