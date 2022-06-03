namespace ZapInjector.ExplicitServicesDeclarationSettings
{
    public class ValueSettings: IExplicitServicesDeclarationSettings
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public TOut AcceptVisitor<TOut, TContext>(IExplicitServicesDeclarationSettingsVisitor<TOut, TContext> visitor) => visitor.Visit(this);


    }
}