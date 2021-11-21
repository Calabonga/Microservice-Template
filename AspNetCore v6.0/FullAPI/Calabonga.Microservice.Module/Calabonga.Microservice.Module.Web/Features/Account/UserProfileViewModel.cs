using System;
using System.Collections.Generic;

namespace Calabonga.Microservice.Module.Web.Features.Account;

/// <summary>
/// Application User Profile
/// </summary>
public class UserProfileViewModel
{
    /// <summary>
    /// Identifier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// FirstName
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// LastName
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// User Roles
    /// </summary>
    public List<string>? Roles { get; set; }

    /// <summary>
    /// User PhoneNumber
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Position Name
    /// </summary>
    public string? PositionName { get; set; }
}