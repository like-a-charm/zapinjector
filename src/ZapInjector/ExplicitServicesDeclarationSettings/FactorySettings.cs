namespace ZapInjector.ExplicitServicesDeclarationSettings
{
    public class FactorySettings: IExplicitServicesDeclarationSettings
    {
        public MethodCallSettings? Method { get; set; }
        public string? StaticReference { get; set; }
        public ParameterSettings? DynamicReference { get; set; }
        public string? InstanceType { get; set; }
        public TOut AcceptVisitor<TOut, TContext>(IExplicitServicesDeclarationSettingsVisitor<TOut, TContext> visitor) => visitor.Visit(this);

    }
}