# ZapInjector

ZapInjector is a class library that helps to configure services into an IServiceCollection.
It provides a way to load all the services defined into a set of assemblies basing on a convention.

1) Define your configuration in the application.json file
```
{
  "ZapInjector": [
    {
      "Strategy": "ZapInjector.Strategies.LoadAllServicesFromAssembliesWithConventionStrategy, ZapInjector",
      "Configuration": {
        "Assemblies": [
          "ZapInjector.Examples.Main",
          "ZapInjector.Examples.Abstractions",
          "ZapInjector.Examples.Implementations"
        ],
        "Convention": "ZapInjector.Strategies.Conventions.LoadAllServicesFromAssembliesDefaultConvention, ZapInjector"
      }
    }
  ]
}
```
2) In the Startup.cs file, in the ConfigureServices  method, call the extension method _LoadFromConfiguration_ 
```
services.LoadFromConfiguration(Configuration);
```