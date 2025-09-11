using Calabonga.AspNetCoreRazorPages.Domain.Base;
using Calabonga.OperationResults;

namespace Calabonga.AspNetCoreRazorPages.Domain.ValueObjects;

public class VersionValue : ValueObject
{
    public const int MaxLength = 16;

    private VersionValue(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Operation<VersionValue, string> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Operation.Error("VersionValue Value is empty");
        }

        if (name.Length > MaxLength)
        {
            return Operation.Error($"VersionValue Value length is greater than {MaxLength}");
        }

        return new VersionValue(name);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
