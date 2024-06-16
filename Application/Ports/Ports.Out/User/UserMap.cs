// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using DomainModel = Application.Domain.Core.User;
//
// namespace Application.Ports.Out.User;
//
// public class UserMap : IEntityTypeConfiguration<DomainModel.User>
// {
//     public void Configure(EntityTypeBuilder<DomainModel.User> builder)
//     {
//         builder.HasKey(e => e.Id);
//
//         builder.Property(c => c.Id)
//             .HasColumnName("Id")
//             .HasConversion(id => id.Value,
//                 value => new DomainModel.UserId(value)
//             );
//
//
//         builder.Property(c => c.Name)
//             .HasColumnType("varchar(100)")
//             .HasMaxLength(100)
//             .IsRequired();
//
//         builder.Property(c => c.Role)
//             .HasColumnType("varchar(100)")
//             .HasMaxLength(100)
//             .IsRequired();
//
//         builder.Property(c => c.Email)
//             .HasColumnType("varchar(100)")
//             .HasMaxLength(100)
//             .IsRequired();
//
//         // builder.HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);
//         builder.HasQueryFilter(p => !p.IsDeleted);
//     }
// }