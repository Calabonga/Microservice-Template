using System;

namespace Calabonga.Microservice.IdentityModule.Web.Infrastructure.Attributes;

/// <summary>
/// Swagger controller group attribute
/// </summary>
///
[AttributeUsage(AttributeTargets.Class)]
public class FeatureGroupNameAttribute : Attribute
{
    /// <inheritdoc />
    public FeatureGroupNameAttribute(string groupName) => GroupName = groupName;

    /// <summary>
    /// Group name
    /// </summary>
    public string GroupName { get; }
}