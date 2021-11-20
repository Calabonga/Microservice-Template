namespace $safeprojectname$.Base;

/// <summary>
/// NamedIdentity dictionary for selector
/// </summary>
public abstract class NamedIdentity : Identity
{
    /// <summary>
    /// Entity name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Entity brief name
    /// </summary>
    public string BriefName { get; set; }

    /// <summary>
    /// Brief descriptions
    /// </summary>
    public string Description { get; set; }
}
