namespace ZapInjector.ExplicitServicesDeclarationSettings
{
    public class ReferenceSettings: IExplicitServicesDeclarationSettings
    {
        public string Name { get; set; }        
        public TOut AcceptVisitor<TOut, TContext>(IExplicitServicesDeclarationSettingsVisitor<TOut, TContext> visitor) => visitor.Visit(this);


    }
}