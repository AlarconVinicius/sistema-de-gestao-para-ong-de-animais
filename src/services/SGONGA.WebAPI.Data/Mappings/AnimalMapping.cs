using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGONGA.WebAPI.Business.Animals.Entities;

namespace SGONGA.WebAPI.Data.Mappings;

public class AnimalMapping : IEntityTypeConfiguration<Animal>
{
    public void Configure(EntityTypeBuilder<Animal> builder)
    {
        builder.ToTable("tbl_animals");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.CreatedAt)
            .IsRequired();

        builder.Property(a => a.UpdatedAt)
            .IsRequired();

        builder.Property(a => a.TenantId)
            .IsRequired();

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(a => a.Species)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.Property(a => a.Breed)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.Property(a => a.Gender)
            .IsRequired()
            .HasColumnType("bit");

        builder.Property(a => a.Neutered)
            .IsRequired()
            .HasColumnType("bit");

        builder.Property(a => a.Color)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.Property(a => a.Size)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.Property(a => a.Age)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(a => a.Description)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnType("varchar(500)");

        builder.Property(a => a.Note)
            .HasMaxLength(500)
            .HasColumnType("varchar(500)");

        builder.Property(a => a.PixKey)
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(a => a.Photo)
            .IsRequired()
            .HasColumnType("text");

        builder.HasOne(a => a.Ngo)
            .WithMany(o => o.Animals)
            .HasForeignKey(a => a.TenantId);
    }
}
