using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DomainModel = Domain.Core.User;

namespace Adapter.Out.Postgres.User;

public class UserMap : IEntityTypeConfiguration<DomainModel.User>
{
    public void Configure(EntityTypeBuilder<DomainModel.User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(c => c.Id)
            .HasColumnName("Id")
            .HasConversion(id => id.Value,
                value => new DomainModel.UserId(value)
            )
            .HasValueGenerator((_, _) => new IdGenerator());
        
        builder.Property(u => u.Role)
            .HasConversion(r => r.ToString(),
                value => (DomainModel.UserRole)Enum.Parse(typeof(DomainModel.UserRole), value)
            );
        
        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}