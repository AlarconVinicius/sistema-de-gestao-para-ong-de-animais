using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Models.DomainObjects;

namespace SGONGA.WebAPI.Data.Mappings;

public class ColaboradorMapping : IEntityTypeConfiguration<Colaborador>
{
    public void Configure(EntityTypeBuilder<Colaborador> builder)
    {
        builder.ToTable("tbl_colaboradores");

        builder.HasKey(a => a.Id);

        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt).IsRequired();

        builder.Property(a => a.TenantId)
            .IsRequired();

        builder.Property(o => o.Nome)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(c => c.Documento)
                .IsRequired()
                .HasMaxLength(11)
                .HasColumnType("varchar(11)");

        builder.Property(c => c.DataNascimento)
                .IsRequired();

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

        builder.HasOne(a => a.ONG)
            .WithMany(o => o.Colaboradores)
            .HasForeignKey(a => a.TenantId);
    }
}