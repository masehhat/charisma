namespace Charisma.Domain;

public record Percent
{
    public Percent(byte value)
    {
        if (value < 0 || value > 100)
            throw new ArgumentOutOfRangeException("percent should be between 0 and 100");
        Value = value;
    }
    public byte Value { get; }
}