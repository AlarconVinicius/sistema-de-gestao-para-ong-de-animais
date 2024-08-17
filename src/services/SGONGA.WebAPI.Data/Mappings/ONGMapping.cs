using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Models.DomainObjects;

namespace SGONGA.WebAPI.Data.Mappings;

public class ONGMapping : IEntityTypeConfiguration<ONG>
{
    public void Configure(EntityTypeBuilder<ONG> builder)
    {
        builder.ToTable("tbl_ongs");

        builder.HasKey(o => o.Id);

        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt).IsRequired();

        builder.Property(o => o.Nome)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(o => o.Instagram)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnType("varchar(200)");

        builder.Property(c => c.Documento)
                .IsRequired()
                .HasMaxLength(11)
                .HasColumnType("varchar(11)");

        builder.Property(o => o.ChavePix)
               .IsRequired(false)
               .HasMaxLength(100)
               .HasColumnType("varchar(100)");

        builder.OwnsOne(o => o.Contato, c =>
        {
            c.OwnsOne(c => c.Telefone, t =>
            {
                t.Property(t => t.Numero)
                    .HasColumnName("Telefone")
                    .HasColumnType("Telefone")
                    .IsRequired()
                    .HasMaxLength(Telefone.ComprimentoMaxNumero)
                    .HasColumnType($"varchar({Telefone.ComprimentoMaxNumero})");
            });

            c.OwnsOne(c => c.Email, t =>
            {
                t.Property(t => t.Endereco)
                    .HasColumnName("Email")
                    .IsRequired()
                    .HasMaxLength(Email.ComprimentoMaxEndereco)
                    .HasColumnType($"varchar({Email.ComprimentoMaxEndereco})");
            });
        });

        builder.OwnsOne(o => o.Endereco, e =>
        {
            e.Property(e => e.Cidade)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar(50)");

            e.Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar(50)");

            e.Property(e => e.CEP)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnType("varchar(20)");

            e.Property(e => e.Logradouro)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            e.Property(e => e.Bairro)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            e.Property(e => e.Numero)
                .IsRequired();

            e.Property(e => e.Complemento)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            e.Property(e => e.Referencia)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");
        });

        builder.HasMany(o => o.Animais)
            .WithOne(a => a.ONG)
            .HasForeignKey(a => a.TenantId);

        builder.HasMany(o => o.Colaboradores)
            .WithOne(c => c.ONG)
            .HasForeignKey(c => c.TenantId);
    }
}
