namespace Domain.Core.Session;

public readonly struct SessionId
{
    private Guid Value { get; }

    SessionId(Guid value)
    {
        Value = value;
    }

    public static SessionId Of(string stringValue)
    {
        return new SessionId(Guid.Parse(stringValue));
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}