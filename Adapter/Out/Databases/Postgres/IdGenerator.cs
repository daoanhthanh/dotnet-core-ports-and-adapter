using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using SnowflakeGenerator;

namespace Adapter.Out.Postgres;

public class IdGenerator: ValueGenerator
{

    private readonly Snowflake _snowflake;

    public IdGenerator()
    {
        Settings settings = new Settings 
        { 
            MachineID = 1,
            CustomEpoch = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero) 
        };
        
        _snowflake = new Snowflake(settings);
    }

    protected override object? NextValue(EntityEntry entry)
    {
        return _snowflake.NextID();
    }

    public override bool GeneratesTemporaryValues { get; } = false;
}