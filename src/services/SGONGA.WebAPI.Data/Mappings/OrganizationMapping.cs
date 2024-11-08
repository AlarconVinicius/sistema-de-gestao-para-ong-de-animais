using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGONGA.WebAPI.Business.People.Entities;

namespace SGONGA.WebAPI.Data.Mappings;

public class OrganizationMapping : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.ToTable("Organizations");

        builder.Property(o => o.PixKey)
               .IsRequired(false)
               .HasMaxLength(100)
               .HasColumnType("varchar(100)");

        builder.HasMany(o => o.Animals)
            .WithOne(a => a.Organization)
            .HasPrincipalKey(a => a.TenantId)
            .HasForeignKey(a => a.TenantId);
    }
}
