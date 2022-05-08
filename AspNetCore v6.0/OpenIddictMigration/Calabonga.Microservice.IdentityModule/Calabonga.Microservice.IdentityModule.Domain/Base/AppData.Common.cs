namespace Calabonga.Microservice.IdentityModule.Domain.Base;

/// <summary>
/// Static data container
/// </summary>
public static partial class AppData
{
    /// <summary>
    /// Current service name
    /// </summary>
    public const string ServiceName = "Microservice Template with OpenIddict";

    /// <summary>
    /// Default policy name
    /// </summary>
    public const string PolicyName = "CorsPolicy";

    /// <summary>
    /// "SystemAdministrator"
    /// </summary>
    public const string SystemAdministratorRoleName = "Administrator";

    /// <summary>
    /// "BusinessOwner"
    /// </summary>
    public const string ManagerRoleName = "Manager";


    /// <summary>
    /// Roles
    /// </summary>
    public static IEnumerable<string> Roles
    {
        get
        {
            yield return SystemAdministratorRoleName;
            yield return ManagerRoleName;
        }
    }
}