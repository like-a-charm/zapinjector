# ZapInjector

[Repository](https://github.com/like-a-charm/zapinjector) | [Nuget Package](https://www.nuget.org/packages/ZapInjector/)

ZapInjector is a class library that helps to configure services into an IServiceCollection.
It provides a way to load all the services defined into a set of assemblies basing on conventions or on explicit rules.

1) Define your configuration in the application.json file
```
{
   "ZapInjector":[
      {
         "Strategy":"ZapInjector.Strategies.LoadAllServicesFromAssembliesWithConventionStrategy, ZapInjector",
         "Configuration":{
            "Assemblies":[
               "ZapInjector.Examples.Main",
               "ZapInjector.Examples.Abstractions",
               "ZapInjector.Examples.Implementations"
            ],
            "Convention":"ZapInjector.Strategies.Conventions.LoadAllServicesFromAssembliesDefaultConvention, ZapInjector"
         }
      },
      {
         "Strategy":"ZapInjector.Strategies.LoadServicesFromExplicitSettingsStrategy, ZapInjector",
         "Configuration":{
            "Values":[
               {
                  "Name":"annasName",
                  "Value":"Anna",
                  "Type":"System.String"
               }
            ],
            "ServiceDescriptions":[
               {
                  "ServiceType":"ZapInjector.Examples.Abstractions.INameAbstraction, ZapInjector.Examples.Abstractions",
                  "ImplementationType":"ZapInjector.Examples.ImplementationsTwo.NameImplementationThree, ZapInjector.Examples.ImplementationsTwo",
                  "ServiceLifetime":"Singleton"
               },
               {
                  "ServiceType":"ZapInjector.Examples.Abstractions.INameAbstraction, ZapInjector.Examples.Abstractions",
                  "ImplementationType":"ZapInjector.Examples.ImplementationsTwo.NameImplementationFour, ZapInjector.Examples.ImplementationsTwo",
                  "Dependencies":[
                     {
                        "Value":{
                           "Value":"Nick",
                           "Type":"System.String"
                        }
                     }
                  ],
                  "ServiceLifetime":"Singleton"
               },
               {
                  "ServiceType":"ZapInjector.Examples.Abstractions.INameAbstraction, ZapInjector.Examples.Abstractions",
                  "ImplementationFactory":{
                     "InstanceType":"ZapInjector.Examples.Abstractions.INameAbstraction, ZapInjector.Examples.Abstractions",
                     "StaticReference":"ZapInjector.Examples.ImplementationsTwo.NameImplementationFour, ZapInjector.Examples.ImplementationsTwo",
                     "Method":{
                        "MethodName":"Create",
                        "Parameters":[
                           {
                              "Value":{
                                 "Value":"Taylor",
                                 "Type":"System.String"
                              }
                           }
                        ]
                     }
                  },
                  "ServiceLifetime":"Singleton"
               },
               {
                  "ServiceType":"ZapInjector.Examples.Abstractions.INameAbstraction, ZapInjector.Examples.Abstractions",
                  "ImplementationType":"ZapInjector.Examples.ImplementationsTwo.NameImplementationFour, ZapInjector.Examples.ImplementationsTwo",
                  "Dependencies":[
                     {
                        "Value":{
                           "Value":"Anna",
                           "Type":"System.String"
                        }
                     }
                  ],
                  "OnAfterCreate":[
                     {
                        "MethodName":"ChangeName",
                        "Parameters":[
                           {
                              "Value":{
                                 "Value":"Claire",
                                 "Type":"System.String"
                              }
                           }
                        ]
                     }
                  ],
                  "ServiceLifetime":"Singleton"
               },
               {
                  "ServiceType":"ZapInjector.Examples.Abstractions.INameAbstraction, ZapInjector.Examples.Abstractions",
                  "ImplementationFactory":{
                     "InstanceType":"ZapInjector.Examples.Abstractions.INameAbstraction, ZapInjector.Examples.Abstractions",
                     "StaticReference":"ZapInjector.Examples.ImplementationsTwo.NameImplementationFour, ZapInjector.Examples.ImplementationsTwo",
                     "Method":{
                        "MethodName":"Create",
                        "Parameters":[
                           {
                              "Value":{
                                 "Value":"Jhon",
                                 "Type":"System.String"
                              }
                           }
                        ]
                     }
                  },
                  "OnAfterCreate":[
                     {
                        "MethodName":"ChangeName",
                        "Parameters":[
                           {
                              "Value":{
                                 "Value":"Johan",
                                 "Type":"System.String"
                              }
                           }
                        ]
                     }
                  ],
                  "ServiceLifetime":"Singleton"
               },
               {
                  "ServiceType":"ZapInjector.Examples.Abstractions.INameAbstraction, ZapInjector.Examples.Abstractions",
                  "ImplementationType":"ZapInjector.Examples.ImplementationsTwo.NameImplementationFour, ZapInjector.Examples.ImplementationsTwo",
                  "Dependencies":[
                     {
                        "Reference":{
                           "Name":"annasName"
                        }
                     }
                  ],
                  "ServiceLifetime":"Singleton"
               }
            ]
         }
      }
   ]
}
```
2) In the Startup.cs file, in the ConfigureServices  method, call the extension method _LoadFromConfiguration_ 
```
services.LoadFromConfiguration(Configuration);
```

A working example can be found [here](https://github.com/like-a-charm/zapinjector/tree/main/examples/ZapInjector.Examples.Main)

## Contributing

The Contributing guide can be found [here](https://github.com/like-a-charm/zapinjector/tree/main/Contributing.md)

## Authors
 - [Daniele De Francesco](https://github.com/danieledefrancesco)