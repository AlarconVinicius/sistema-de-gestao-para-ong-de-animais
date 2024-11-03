using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGONGA.WebAPI.Business.Models.DomainObjects;
using SGONGA.WebAPI.Business.People.Entities;

namespace SGONGA.WebAPI.Data.Mappings;

public class UsuarioMapping : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("tbl_usuarios");

        builder.HasKey(a => a.Id);

        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt).IsRequired();

        builder.Property(a => a.TenantId)
            .IsRequired();

        builder.Property(o => o.UsuarioTipo)
            .IsRequired();

        builder.Property(o => o.TelefoneVisivel)
            .IsRequired();

        builder.Property(o => o.AssinarNewsletter)
            .IsRequired();

        builder.Property(o => o.Nome)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(o => o.Apelido)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(o => o.Slug)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(c => c.Documento)
                .IsRequired()
                .HasMaxLength(11)
                .HasColumnType("varchar(11)");

        builder.Property(o => o.Site)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnType("varchar(200)");

        builder.Property(e => e.Cidade)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar(50)");

        builder.Property(e => e.Estado)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.Property(e => e.Sobre)
            .IsRequired(false)
            .HasColumnType("text");

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
    }
}