using Demo.Core.Domain.Users;

namespace Demo.Core.Infrastructure.Repository;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(e => e.Id)
            .HasConversion(x => x.Value, x => UserId.CreateInstance(x));

        builder
            .OwnsOne(p => p.Name, name => {
                name.Property(p => p.FirstName).HasColumnName("first_name");
                name.Property(p => p.LastName).HasColumnName("last_name");
            });

        builder.Property(p => p.Email).HasColumnName("email");
        builder.Property(p => p.Password).HasColumnName("password");

        builder
            .Ignore(x => x.DomainEvents);
    }
}