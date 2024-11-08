using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGONGA.WebAPI.Business.People.Entities;
using SGONGA.WebAPI.Business.People.ValueObjects;

namespace SGONGA.WebAPI.Data.Mappings;

public class PersonMapping : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("People");

        builder.HasKey(a => a.Id);

        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt).IsRequired();

        builder.Property(a => a.TenantId)
            .IsRequired();

        builder.Property(o => o.PersonType)
            .IsRequired();

        builder.Property(o => o.IsPhoneNumberVisible)
            .IsRequired();

        builder.Property(o => o.SubscribeToNewsletter)
            .IsRequired();

        builder.Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(o => o.Nickname)
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

        builder.OwnsOne(c => c.PhoneNumber, t =>
        {
            t.Property(t => t.Number)
                .HasColumnName("PhoneNumber")
                .IsRequired()
                .HasMaxLength(PhoneNumber.LengthWithDDD)
                .HasColumnType($"varchar({PhoneNumber.LengthWithDDD})");
        });

        builder.OwnsOne(c => c.Document, t =>
        {
            t.Property(t => t.Number)
                .HasColumnName("Document")
                .IsRequired()
                .HasMaxLength(Document.CnpjLength)
                .HasColumnType($"varchar({Document.CnpjLength})");
        });

        builder.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar(50)");

        builder.Property(e => e.State)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.Property(e => e.About)
            .IsRequired(false)
            .HasMaxLength(500)
            .HasColumnType("varchar(500)");

        builder.Property(c => c.BirthDate)
                .IsRequired();
    }
}