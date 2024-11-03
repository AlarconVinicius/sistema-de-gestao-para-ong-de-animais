using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGONGA.WebAPI.Business.Models;

namespace SGONGA.WebAPI.Data.Mappings;

public class AdotanteMapping : IEntityTypeConfiguration<Adopter>
{
    public void Configure(EntityTypeBuilder<Adopter> builder)
    {
        builder.ToTable("tbl_adotantes");

    }
}
