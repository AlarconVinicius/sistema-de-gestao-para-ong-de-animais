using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGONGA.WebAPI.Business.People.Entities;
using SGONGA.WebAPI.Business.People.ValueObjects;

namespace SGONGA.WebAPI.Data.Mappings;

public class PersonMapping : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("tbl_people");

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

        builder.OwnsOne(o => o.Slug, p =>
        {
            p.Property(p => p.UrlPath)
            .HasColumnName("Slug")
            .IsRequired()
            .HasMaxLength(Slug.MaxLength)
            .HasColumnType($"varchar({Slug.MaxLength})");
        });

        builder.OwnsOne(o => o.Site, p =>
        {
            p.Property(p => p.Url)
            .HasColumnName("Site")
            .IsRequired()
            .HasMaxLength(Site.MaxLength)
            .HasColumnType($"varchar({Site.MaxLength})");
        });

        builder.OwnsOne(c => c.Email, t =>
        {
            t.Property(t => t.Address)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(Email.MaxLength)
                .HasColumnType($"varchar({Email.MaxLength})");
        });

        builder.OwnsOne(c => c.Telefone, t =>
        {
            t.Property(t => t.Number)
                .HasColumnName("Telefone")
                .HasColumnType("Telefone")
                .IsRequired()
                .HasMaxLength(PhoneNumber.LengthWithDDD)
                .HasColumnType($"varchar({PhoneNumber.LengthWithDDD})");
        });

        builder.OwnsOne(c => c.Documento, t =>
        {
            t.Property(t => t.Number)
                .HasColumnName("Documento")
                .IsRequired()
                .HasMaxLength(Document.CnpjLength)
                .HasColumnType($"varchar({Document.CnpjLength})");
        });

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
    }
}