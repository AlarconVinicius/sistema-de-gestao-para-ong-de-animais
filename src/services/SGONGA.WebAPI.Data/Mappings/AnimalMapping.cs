using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGONGA.WebAPI.Business.Animals.Entities;

namespace SGONGA.WebAPI.Data.Mappings;

public class AnimalMapping : IEntityTypeConfiguration<Animal>
{
    public void Configure(EntityTypeBuilder<Animal> builder)
    {
        builder.ToTable("tbl_animais");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.CreatedAt)
            .IsRequired();

        builder.Property(a => a.UpdatedAt)
            .IsRequired();

        builder.Property(a => a.TenantId)
            .IsRequired();

        builder.Property(a => a.Nome)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(a => a.Especie)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.Property(a => a.Raca)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.Property(a => a.Sexo)
            .IsRequired()
            .HasColumnType("bit");

        builder.Property(a => a.Castrado)
            .IsRequired()
            .HasColumnType("bit");

        builder.Property(a => a.Cor)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.Property(a => a.Porte)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.Property(a => a.Idade)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(a => a.Descricao)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnType("varchar(500)");

        builder.Property(a => a.Observacao)
            .HasMaxLength(500)
            .HasColumnType("varchar(500)");

        builder.Property(a => a.ChavePix)
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(a => a.Foto)
            .IsRequired()
            .HasColumnType("text");

        builder.HasOne(a => a.ONG)
            .WithMany(o => o.Animais)
            .HasForeignKey(a => a.TenantId);
    }
}
