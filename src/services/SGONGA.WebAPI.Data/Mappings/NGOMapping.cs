using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGONGA.WebAPI.Business.People.Entities;

namespace SGONGA.WebAPI.Data.Mappings;

public class NGOMapping : IEntityTypeConfiguration<NGO>
{
    public void Configure(EntityTypeBuilder<NGO> builder)
    {
        builder.ToTable("tbl_ngos");

        builder.Property(o => o.PixKey)
               .IsRequired(false)
               .HasMaxLength(100)
               .HasColumnType("varchar(100)");

        builder.HasMany(o => o.Animals)
            .WithOne(a => a.ONG)
            .HasPrincipalKey(a => a.TenantId)
            .HasForeignKey(a => a.TenantId);
    }
}
