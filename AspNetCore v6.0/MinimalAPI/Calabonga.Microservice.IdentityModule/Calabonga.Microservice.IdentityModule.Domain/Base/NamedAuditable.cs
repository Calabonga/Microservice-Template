namespace Calabonga.Microservice.IdentityModule.Domain.Base
{
    /// <summary>
    /// Audit-able with name
    /// </summary>
    public abstract class NamedAuditable : Auditable
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}