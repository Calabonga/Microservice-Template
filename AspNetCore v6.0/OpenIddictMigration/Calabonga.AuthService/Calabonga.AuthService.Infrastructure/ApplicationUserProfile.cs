namespace Calabonga.AuthService.Infrastructure
{
    /// <summary>
    /// Represent person with login information (ApplicationUser)
    /// </summary>
    public class ApplicationUserProfile : Auditable
    {
        /// <summary>
        /// Account
        /// </summary>
        public virtual ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// Microservice permission for policy-based authorization
        /// </summary>
        public ICollection<MicroservicePermission> Permissions { get; set; }
    }
}