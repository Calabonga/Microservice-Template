namespace Calabonga.Microservice.RazorPages.Domain.Base;

/// <summary>
/// Identifier
/// </summary>
public abstract class Identity : IHaveId
{
    /// <summary>
    /// Identifier
    /// </summary>
    public Guid Id { get; set; }
}
