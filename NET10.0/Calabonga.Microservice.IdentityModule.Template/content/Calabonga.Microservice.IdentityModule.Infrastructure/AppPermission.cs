using Calabonga.Microservice.IdentityModule.Domain.Base;

namespace Calabonga.Microservice.IdentityModule.Infrastructure;

/// <summary>
/// User permission for application
/// </summary>
public class AppPermission : Auditable
{
    /// <summary>
    /// Application User profile identifier
    /// </summary>
    public Guid ApplicationUserProfileId { get; set; }

    /// <summary>
    /// Application User Profile
    /// </summary>
    public virtual ApplicationUserProfile? ApplicationUserProfile { get; set; }

    /// <summary>
    /// Authorize attribute policy name
    /// </summary>
    public string PolicyName { get; set; } = null!;

    /// <summary>
    /// Description for current permission
    /// </summary>
    public string Description { get; set; } = null!;
}
