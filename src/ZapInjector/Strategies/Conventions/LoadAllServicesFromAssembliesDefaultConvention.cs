using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using ZapInjector.Utils;

namespace ZapInjector.Strategies.Conventions
{
    public class LoadAllServicesFromAssembliesDefaultConvention: ILoadAllServicesFromAssembliesConvention
    {
        public IEnumerable<Type> GetAllTypesToRegisterForService(Type service, IEnumerable<Type> allTypes)
        {
            if (!(service.IsNonGenericInterface() || service.IsNonGenericConcreteClass()))
            {
                return Enumerable.Empty<Type>();
            }
            return allTypes.Where(concreteType =>
                concreteType.IsNonGenericConcreteClass() && concreteType.InheritsOrImplementsOrIs(service));

        }

        public ServiceLifetime DefaultServiceLifetime => ServiceLifetime.Scoped;
    }
}