using System;
using FluentAssertions;
using NUnit.Framework;
using ZapInjector.Utils;

namespace ZapInjector.Test.Utils
{
    public class TypeUtilsTest
    {
        interface INonGenericInterface {}
        abstract class NonGenericAbstractClass : INonGenericInterface {}
        class NonGenericClass : NonGenericAbstractClass {}
        interface IGenericInterface<T> {}
        abstract class GenericAbstractClass<T> : IGenericInterface<T> {}
        class GenericClass<T> : GenericAbstractClass<T> {}
        
        [Test]
        public void IsNonGenericInterface_ShouldBeTrue_IfTypeIsNonGenericInterface()
        {
            typeof(INonGenericInterface).IsNonGenericInterface().Should().BeTrue();
        }
        [TestCase(typeof(NonGenericAbstractClass))]
        [TestCase(typeof(NonGenericClass))]
        [TestCase(typeof(IGenericInterface<>))]
        [TestCase(typeof(IGenericInterface<string>))]
        [TestCase(typeof(GenericAbstractClass<>))]
        [TestCase(typeof(GenericAbstractClass<string>))]
        [TestCase(typeof(GenericClass<>))]
        [TestCase(typeof(GenericClass<string>))]
        public void IsNonGenericInterface_ShouldBeFalse_IfTypeIsNotANonGenericInterface(Type type)
        {
            type.IsNonGenericInterface().Should().BeFalse();
        }
        
        [Test]
        public void IsNonGenericConcreteClass_ShouldBeTrue_IfTypeIsNonGenericConcreteClass()
        {
            typeof(NonGenericClass).IsNonGenericConcreteClass().Should().BeTrue();
        }
        
        [TestCase(typeof(INonGenericInterface))]
        [TestCase(typeof(NonGenericAbstractClass))]
        [TestCase(typeof(IGenericInterface<>))]
        [TestCase(typeof(IGenericInterface<string>))]
        [TestCase(typeof(GenericAbstractClass<>))]
        [TestCase(typeof(GenericAbstractClass<string>))]
        [TestCase(typeof(GenericClass<>))]
        [TestCase(typeof(GenericClass<string>))]
        public void IsNonGenericConcreteClass_ShouldBeFalse_IfTypeIsNotANonGenericConcreteClass(Type type)
        {
            type.IsNonGenericConcreteClass().Should().BeFalse();
        }

        [TestCase(typeof(INonGenericInterface), typeof(INonGenericInterface))]
        [TestCase(typeof(NonGenericAbstractClass), typeof(INonGenericInterface))]
        [TestCase(typeof(NonGenericClass), typeof(INonGenericInterface))]
        public void InheritsOrImplementsOrIs_ShouldBeTrue_IfType1InheritsOrImplementsOrIsType2(Type type1, Type type2)
        {
            type1.InheritsOrImplementsOrIs(type2).Should().BeTrue();
        }
        [TestCase(typeof(INonGenericInterface), typeof(NonGenericAbstractClass))]
        [TestCase(typeof(NonGenericAbstractClass), typeof(NonGenericClass))]
        public void InheritsOrImplementsOrIs_ShouldBeFalse_IfType1DoesntInheritOrImplementsrIsType2(Type type1, Type type2)
        {
            type1.InheritsOrImplementsOrIs(type2).Should().BeFalse();
        }
    }
}