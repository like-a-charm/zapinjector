using FluentAssertions;
using NUnit.Framework;
using ZapInjector.Utils;

namespace ZapInjector.Test.Utils
{
    public class AssembliesExportedTypesAccessorTest
    {
        private AssembliesExportedTypesAccessor _assembliesExportedTypesAccessor;

        [SetUp]
        public void SetUp()
        {
            _assembliesExportedTypesAccessor = new AssembliesExportedTypesAccessor();
        }

        [Test]
        public void GetExportedTypesFromAllAssembliesNames_ShouldNotReturnAnEmptyResult()
        {
            _assembliesExportedTypesAccessor.GetExportedTypesFromAllAssembliesNames(new[] { "ZapInjector.Test" })
                .Should().NotBeEmpty();
        }
    }
}