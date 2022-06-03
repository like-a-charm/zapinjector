using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using static ZapInjector.Test.TestUtils;
namespace ZapInjector.Test
{
    public class ExtensionsTest
    {
        [Test]
        public void LoadFromConfiguration_ShouldNotThrowException()
        {
            IConfiguration configuration = new ConfigurationBuilder().Build();
            IServiceCollection services = new ServiceCollection();
            Act(() => services.LoadFromConfiguration(configuration))
                .Should().NotThrow();
        }
    }
}