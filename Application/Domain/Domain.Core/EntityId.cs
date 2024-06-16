namespace Domain.Core;

public abstract class EntityId
{
    protected bool Equals(EntityId other)
    {
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((EntityId)obj);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public abstract long Value { get; }

    public static bool operator ==(EntityId? from, EntityId? to)
    {
        return from?.Value == to?.Value;
    }
    
    public static bool operator !=(EntityId? from, EntityId? to)
    {
        return from?.Value != to?.Value;
    }
}