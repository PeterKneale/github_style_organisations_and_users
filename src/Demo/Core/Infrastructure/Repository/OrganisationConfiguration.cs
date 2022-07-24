using Demo.Core.Domain.Organisations;
using Demo.Core.Domain.Users;

namespace Demo.Core.Infrastructure.Repository;

public class OrganisationConfiguration : IEntityTypeConfiguration<Organisation>
{
    public void Configure(EntityTypeBuilder<Organisation> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(e => e.Id)
            .HasConversion(x => x.Value, x => OrganisationId.CreateInstance(x));

        builder
            .OwnsOne(p => p.Title, name => {
                name.Property(p => p.Title).HasColumnName("title");
                name.Property(p => p.Description).HasColumnName("description");
            });
        
        builder
            .OwnsMany<OrganisationMember>("_members", builder =>
            {
                builder
                    .Property(e => e.Id)
                    .HasConversion(x => x.Value, x => OrganisationMemberId.CreateInstance(x));
        
                builder
                    .Property(e => e.OrganisationId)
                    .HasConversion(x => x.Value, x => OrganisationId.CreateInstance(x));
        
                builder
                    .Property(e => e.UserId)
                    .HasConversion(x => x.Value, x => UserId.CreateInstance(x))
                    .HasColumnName("member_id");
                
                builder.OwnsOne(x=>x.Role, b =>
                {
                    b.Property(x => x.Value).HasColumnName("role");
                });
                
                builder
                    .Ignore(x => x.DomainEvents);
            });
        
        builder
            .Ignore(x => x.DomainEvents);
    }
}