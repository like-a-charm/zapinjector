namespace ZapInjector.ExplicitServicesDeclarationSettings
{
    public class ParameterSettings: IExplicitServicesDeclarationSettings
    {
        public ReferenceSettings? Reference { get; set; }
        public ServiceDescriptionSettings? ServiceDescription { get; set; }
        public ValueSettings? Value { get; set; }
        public TOut AcceptVisitor<TOut, TContext>(IExplicitServicesDeclarationSettingsVisitor<TOut, TContext> visitor) => visitor.Visit(this);


    }
}