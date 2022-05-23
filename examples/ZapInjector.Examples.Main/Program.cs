using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZapInjector.Examples.Main;

var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables();

var configuration = builder.Build();
IServiceCollection services = new ServiceCollection();
services.LoadFromConfiguration(configuration);

var serviceProvider = services.BuildServiceProvider();
serviceProvider.GetRequiredService<HelloService>().SayHelloToEveryone();