using System;

namespace ZapInjector.Utils
{
    public static class TypeUtils
    {
        public static bool IsNonGenericInterface(this Type type) => type.IsInterface && !type.IsGenericType;

        public static bool IsNonGenericConcreteClass(this Type type) =>
            type.IsClass && !type.IsAbstract && !type.IsGenericType;

        public static bool InheritsOrImplementsOrIs(this Type subType, Type superType) => superType.IsAssignableFrom(subType);
    }
}