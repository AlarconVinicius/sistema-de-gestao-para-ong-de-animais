using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGONGA.WebAPI.Business.Models;

namespace SGONGA.WebAPI.Data.Mappings;

public class ONGMapping : IEntityTypeConfiguration<ONG>
{
    public void Configure(EntityTypeBuilder<ONG> builder)
    {
        builder.ToTable("tbl_ongs");

        builder.Property(o => o.ChavePix)
               .IsRequired(false)
               .HasMaxLength(100)
               .HasColumnType("varchar(100)");

        builder.HasMany(o => o.Animais)
            .WithOne(a => a.ONG)
            .HasPrincipalKey(a => a.TenantId)
            .HasForeignKey(a => a.TenantId);
    }
}
