using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGONGA.WebAPI.Business.Models;

namespace SGONGA.WebAPI.Data.Mappings;

public class AdotanteMapping : IEntityTypeConfiguration<Adotante>
{
    public void Configure(EntityTypeBuilder<Adotante> builder)
    {
        builder.ToTable("tbl_adotantes");

    }
}
