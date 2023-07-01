namespace Domain.ValueObjects;

public record Sku
{
    private const int defaultLenght = 8;
    private Sku(string value) => Value = value;
    public string Value { get; init; }

    public static Sku? Create(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length != defaultLenght)
        {
            return null;
        }

        return new Sku(value);
    }

}