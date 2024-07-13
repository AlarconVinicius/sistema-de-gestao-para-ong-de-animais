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

        builder.OwnsOne(o => o.Email, c =>
        {
            c.Property(c => c.Endereco)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(Email.ComprimentoMaxEndereco);
        });

        builder.HasOne(a => a.ONG)
            .WithMany(o => o.Colaboradores)
            .HasForeignKey(a => a.TenantId);
    }
}