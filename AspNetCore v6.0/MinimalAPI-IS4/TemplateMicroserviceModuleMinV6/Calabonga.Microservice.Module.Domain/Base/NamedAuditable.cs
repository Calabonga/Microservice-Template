namespace $safeprojectname$.Base
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