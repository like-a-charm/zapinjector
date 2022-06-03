using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ZapInjector.Utils
{
    public class AssembliesExportedTypesAccessor: IAssembliesExportedTypesAccessor
    {
        public IEnumerable<Type> GetExportedTypesFromAllAssembliesNames(IEnumerable<string> assemblies) => assemblies
                .Select(GetAssemblyFromName)
                .SelectMany(assembly => assembly.GetExportedTypes());

        private Assembly GetAssemblyFromName(string assemblyName) =>
            AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SingleOrDefault(assembly => assembly.GetName().Name == assemblyName) ??
            Assembly.Load(assemblyName) ??
            throw new InvalidOperationException($"Could not find assembly {assemblyName}");

    }
}