# ZapInjector

[Repository](https://github.com/like-a-charm/zapinjector) | [Nuget Package](https://www.nuget.org/packages/ZapInjector/)

ZapInjector is a class library that helps to configure services into an IServiceCollection.
It provides a way to load all the services defined into a set of assemblies basing on conventions or on explicit rules.

ZapInjector solves some problems that are commonly faced when using the default .NET Dependency Injection
1. Scans assemblies and loads services basing on conventions without having to declare them in the Startup.cs
2. Helps to set up complex services declarations through configurations rather than through code
3. Helps to deal with configuration values and environment variables

## Table of contents

1. [Installation](#configuration)
2. [Usage](#usage)
   1. [Using Yaml](#using-yaml)
3. [Strategies](#strategies)
4. [Load all services with convention strategy](#load-all-services-with-convention-strategy)
   1. [Default convention](#default-convention)
5. [Load all services from explicit settings strategy](#load-all-services-from-explicit-settings-strategy)
   1. [Services Descriptions](#services-descriptions)
   2. [Parameters](#parameters) 
   3. [Values](#values)
   4. [References](#references)
   5. [Factories](#factories)
   6. [Method Calls](#method-calls)
6. [Future Improvements](#future-improvements)
7. [Contributing](#contributing)

## Installation

Install using the [ZapInjector package](https://www.nuget.org/packages/ZapInjector/)

```
PM> Install-Package ZapInjector
```

## Usage

When you install the package, it should be added to your _csproj_ file. Alternatively, you can add it directly by adding:

```xml
<PackageReference Include="ZapInjector" Version="1.0.1" />
```

Define your configuration in the application.json or in the application.yaml files as done in [this example](https://github.com/like-a-charm/zapinjector/tree/main/examples/ZapInjector.Examples.Main)
In the Startup.cs file, in the ConfigureServices  method, call the extension method _LoadFromConfiguration_ 

```c#
services.LoadFromConfiguration(Configuration);
```


### Using Yaml
It is possible to convert the application.json file to a yaml file if you prefer.
The example above uses [NetEscapades.Configuration.Yaml](https://github.com/andrewlock/NetEscapades.Configuration) as configuration provider for yaml files.

## Strategies

The root elements of the ZapInjector's configuration are strategies. It is possible to define multiple strategies in the same configuration.

The example below shows how the root of the configuration should look like:

```json
{
   "ZapInjector": [
      {
         "Strategy":  "ZapInjector.Strategies.FirstStrategyName, ZapInjector",
         "Configuration": {
            // Strategy configuration
         }
      },
      {
         "Strategy":  "ZapInjector.Strategies.SecondStrategyName, ZapInjector",
         "Configuration": {
            // Strategy configuration
         }
      }
   ]
}
```

## Load all services with convention strategy

The goal of **Load all services with convention strategy** is to load all the services of the given assemblies basing on a convention.

```json
{
   "ZapInjector": [
      {
         "Strategy": "ZapInjector.Strategies.LoadAllServicesFromAssembliesWithConventionStrategy, ZapInjector",
         "Configuration": {
            "Assemblies": [
               "Assembly1",
               "Assembly2"
            ],
            "Convention": "ZapInjector.Strategies.Conventions.ConventionName, ZapInjector"
         }
      }
   ]
}
```

### Default Convention
 The `LoadAllServicesFromAssembliesDefaultConvention` will add a ServiceDescriptor to the ServiceCollection for every concrete non-generic implementation of a valid service type found in the exported types of the assemblies. A valid service type is either a non-generic interface or a non-generic concrete class. If multiple implementations are found for a given service type, they can be injected as `IEnumerable<ServiceType>`

## Load all services from explicit settings strategy

The goal of **Load all services with convention strategy** is to provide developers a way to deal with complex services declarations through configuration files rather than through code.

```json
{
   "ZapInjector": [
      {
         "Strategy": "ZapInjector.Strategies.LoadServicesFromExplicitSettingsStrategy, ZapInjector",
         "Configuration": {
            "Values":[
               {
                  // a valid value object. Check the Values section below
               }
            ],
            "ServiceDescriptions":[
               {
                  // a valid service description object. Check the Service Descriptions section below.
               }
            ]
         }
      }
   ]
}
```

### Service Descriptions

The Service Description object has the following properties

| Name | Type | Is Mandatory | Description | Example | Notes |
| ---  | ---  | ---          | ---         | ---     | ---   |
| ServiceName | string | false | The name to use to reference this service | MyServiceName | If this property is provided, the service can be referenced from a [Reference](#references) object
| ServiceType | string | true | The full name of the service type | ZapInjector.Abstractions.INameAbstraction, ZapInjector.Abstractions |
| ImplementationType | string | false | The full name of the implementation type | ZapInjector.Implementations.NameImplementation, ZapInjector.Implementations | Only one of ImplementationType and ImplementationFactory can be provided
| ImplementationFactory | [Factory](#factories) | false | The factory which creates the implementation instance |  | Only one of ImplementationType and ImplementationFactory can be provided
| Dependencies | Array of [Parameter](#parameters) | false | The service dependencies that will be provided from the configuration instead of from the service provider |  |
| OnAfterCreate | Array of [MethodCall](#method-calls) | false | Methods to be called on the implementation instance after it's created |
| ServiceLifetime | string | false | The service lifetime. The allowed values are `Singleton`, `Scoped` and `Transient`. The default value is `Scoped`. | Scoped | |


### Parameters

The Parameter object has the following properties

| Name | Type | Is Mandatory | Description | Example | Notes |
| ---  | ---  | ---          | ---         | ---     | ---   |
| Reference | [Reference](#references) | false | The Reference object | | Only one of Reference, ServiceDescription and Value can be provided |
| ServiceDescription | [ServiceDescription](#services-descriptions) | false | The ServiceDescription object | Only one of Reference, ServiceDescription and Value can be provided |
| Value | [Value](#values) | false | The Value object | Only one of Reference, ServiceDescription and Value can be provided |

### Values

The Value object has the following properties

| Name | Type | Is Mandatory | Description | Example | Notes |
| ---  | ---  | ---          | ---         | ---     | ---   |
| Name | string | false | The name to use to reference this value | MyValueName | If this property is provided, the value can be referenced from a [Reference](#references) object
| Type | string | true | The name of the type of the value | System.String | Only primitive types are supported |
| Value | string | true | The value | MY_STRING | It can be a string like `MY_STRING` or it can reference an environment variable using `env(ENVIRONMENT_VARIABLE_NAME)` 

### References

The Reference object has the following properties

| Name | Type | Is Mandatory | Description | Example | Notes |
| ---  | ---  | ---          | ---         | ---     | ---   |
| Name | string | true | The name of the referenced ServiceDescription or Value object | MyReferenceName | 

### Factories

### Method Calls

## Future Improvements

## Contributing

The Contributing guide can be found [here](https://github.com/like-a-charm/zapinjector/tree/main/Contributing.md)

## Authors
 - [Daniele De Francesco](https://github.com/danieledefrancesco)