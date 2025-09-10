using Calabonga.Microservice.RazorPages.Domain.Base;
using Calabonga.OperationResults;

namespace Calabonga.Microservice.RazorPages.Domain.ValueObjects;

public class TitleValue : ValueObject
{
    public const int MaxLength = 512;

    private TitleValue(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Operation<TitleValue, string> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Operation.Error("Title Value is empty");
        }

        if (name.Length > MaxLength)
        {
            return Operation.Error($"Title Value length is greater than {MaxLength}");
        }

        return new TitleValue(name);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

