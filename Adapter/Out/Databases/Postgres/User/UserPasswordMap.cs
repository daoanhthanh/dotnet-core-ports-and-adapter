using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DomainModel = Domain.Core.User;

namespace Adapter.Out.Postgres.User;

public class UserPasswordMap : IEntityTypeConfiguration<DomainModel.UserPassword>
{
    public void Configure(EntityTypeBuilder<DomainModel.UserPassword> builder)
    {
        builder.Property(c => c.UserId)
            .HasColumnName("UserId")
            .HasConversion(id => id.Value,
                value => new DomainModel.UserId(value)
            );

        builder.HasKey(s => s.Id);

        builder.Property(p => p.Id)
            .HasValueGenerator((_, _) => new IdGenerator());
    }
}