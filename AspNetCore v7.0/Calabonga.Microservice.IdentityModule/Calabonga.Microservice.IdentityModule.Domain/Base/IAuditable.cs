namespace Calabonga.Microservice.IdentityModule.Domain.Base;

/// <summary>
/// Represent information about creation and last update
/// </summary>
public interface IAuditable
{
    /// <summary>
    /// DateTime of creation. This value will never changed
    /// </summary>
    DateTime CreatedAt { get; set; }

    /// <summary>
    /// Author name. This value never changed
    /// </summary>
    string CreatedBy { get; set; }

    /// <summary>
    /// DateTime of last value update. Should be updated when entity data updated
    /// </summary>
    DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Author of last value update. Should be updated when entity data updated
    /// </summary>
    string UpdatedBy { get; set; }

}