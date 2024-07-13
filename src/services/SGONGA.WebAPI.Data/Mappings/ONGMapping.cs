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
            .HasMaxLength(100);

        builder.Property(o => o.Descricao)
            .IsRequired()
            .HasMaxLength(500);

        builder.OwnsOne(o => o.Contato, c =>
        {
            c.OwnsOne(c => c.Telefone, t =>
            {
                t.Property(t => t.Numero)
                    .HasColumnName("Telefone")
                    .IsRequired()
                    .HasMaxLength(Telefone.ComprimentoMaxNumero);
            });

            c.OwnsOne(c => c.Email, t =>
            {
                t.Property(t => t.Endereco)
                    .HasColumnName("Email")
                    .IsRequired()
                    .HasMaxLength(Email.ComprimentoMaxEndereco);
            });
        });

        builder.OwnsOne(o => o.Endereco, e =>
        {
            e.Property(e => e.Rua)
                .IsRequired()
                .HasMaxLength(100);

            e.Property(e => e.Cidade)
                .IsRequired()
                .HasMaxLength(50);

            e.Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(50);

            e.Property(e => e.CEP)
                .IsRequired()
                .HasMaxLength(20);

            e.Property(e => e.Complemento)
                .HasMaxLength(100);
        });

        builder.Property(o => o.ChavePix)
            .HasMaxLength(100);

        builder.HasMany(o => o.Animais)
            .WithOne(a => a.ONG)
            .HasForeignKey(a => a.TenantId);

        builder.HasMany(o => o.Colaboradores)
            .WithOne(c => c.ONG)
            .HasForeignKey(c => c.TenantId);
    }
}
