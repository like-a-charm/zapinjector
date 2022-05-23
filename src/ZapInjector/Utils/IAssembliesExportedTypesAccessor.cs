using System;
using System.Collections.Generic;

namespace ZapInjector.Utils
{
    public interface IAssembliesExportedTypesAccessor
    {
        IEnumerable<Type> GetExportedTypesFromAllAssembliesNames(IEnumerable<string> assemblies);
    }
}