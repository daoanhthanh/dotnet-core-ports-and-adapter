namespace Domain.Core.User;

public class UserId(long value) : EntityId
{
    public override long Value { get;} = value;
}