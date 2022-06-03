using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using ZapInjector.ExplicitServicesDeclarationSettings;

namespace ZapInjector.Test.ExplicitServicesDeclarationSettings.ComputeServiceFactoriesVisitorTest
{
    public static class StaticFactory
    {
        public static object StaticCreate(string argument)
        {
            return $"static-created-{argument}";
        }
    }
    public class DynamicFactory
    {
        public object Create(string argument)
        {
            return $"created-{argument}";
        }
    }
    
    public class VisitFactorySettingsTest
    {
        private ComputeServiceFactoriesVisitor _computeServiceFactoriesVisitor;
        private IComputeServiceFactoriesContext _computeServiceFactoriesContext;

        [SetUp]
        public void SetUp()
        {
            _computeServiceFactoriesContext = Substitute.For<IComputeServiceFactoriesContext>();
            _computeServiceFactoriesVisitor = Substitute.ForPartsOf<ComputeServiceFactoriesVisitor>(_computeServiceFactoriesContext);
        }

        [Test]
        public void VisitFactorySettings_CreatesInstanceFromStaticMethod_IfStaticReferenceIsProvided()
        {
            var factorySettings = new FactorySettings
            {
                Method = new MethodCallSettings
                {
                    Parameters = new []
                    {
                        new ParameterSettings
                        {
                            Value = new ValueSettings
                            {
                                Type = "System.String",
                                Value = "value"
                            }
                        }
                    },
                    MethodName = "StaticCreate"
                },
                StaticReference = $"{typeof(StaticFactory)}, {typeof(StaticFactory).Assembly.GetName().Name}",
                InstanceType = "System.Object"
                
            };

            var serviceFactory = _computeServiceFactoriesVisitor.Visit(factorySettings);

            serviceFactory.Should().NotBeNull();
            serviceFactory.ObjectFactory!.Invoke(null, null)
                .Should().Be("static-created-value");

        }
        
        [Test]
        public void VisitFactorySettings_CreatesInstanceFromInstanceMethod_IfDynamicReferenceIsProvided()
        {
            var methodArgumentParameterSettings = new ParameterSettings();
            var dynamicReferenceParameterSettings = new ParameterSettings();
            
            var factorySettings = new FactorySettings
            {
                Method = new MethodCallSettings
                {
                    Parameters = new []
                    {
                        methodArgumentParameterSettings
                    },
                    MethodName = "Create"
                },
                DynamicReference = dynamicReferenceParameterSettings,
                InstanceType = "System.Object"
                
            };

            var dynamicReferenceServiceFactory = new ServiceFactory()
            {
                Type = typeof(DynamicFactory),
                ObjectFactory = (sp, args) => new DynamicFactory()
            };
            var methodArgumentServiceFactory = new ServiceFactory()
            {
                Type = typeof(string),
                ObjectFactory = (sp, args) => "value"
            };

            _computeServiceFactoriesVisitor.Visit(Arg.Any<ParameterSettings>()).Returns(info => info[0] == dynamicReferenceParameterSettings ? dynamicReferenceServiceFactory : methodArgumentServiceFactory);
                
            _computeServiceFactoriesVisitor.ClearReceivedCalls();
            
            var serviceFactory = _computeServiceFactoriesVisitor.Visit(factorySettings);

            serviceFactory.Should().NotBeNull();
            serviceFactory.ObjectFactory!.Invoke(null, null)
                .Should().Be("created-value");
            
            _computeServiceFactoriesVisitor.Received(2).Visit(Arg.Any<ParameterSettings>());
        }
    }
}