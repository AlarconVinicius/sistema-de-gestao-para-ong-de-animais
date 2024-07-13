using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGONGA.WebAPI.Business.Models;

namespace SGONGA.WebAPI.Data.Mappings;

public class AnimalMapping :IEntityTypeConfiguration<Animal>
{
    public void Configure(EntityTypeBuilder<Animal> builder)
    {
        builder.ToTable("tbl_animais");

        builder.HasKey(a => a.Id);

        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt).IsRequired();

        builder.Property(a => a.TenantId)
            .IsRequired();

        builder.Property(a => a.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Especie)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.Raca)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.Cor)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.Porte)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.Descricao)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(a => a.Observacao)
            .HasMaxLength(500);

        builder.Property(a => a.ChavePix)
            .HasMaxLength(100);

        builder.Property(a => a.Fotos)
            .HasConversion(
                v => string.Join(';', v),
                v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList());

        builder.HasOne(a => a.ONG)
            .WithMany(o => o.Animais)
            .HasForeignKey(a => a.TenantId);
    }
}
