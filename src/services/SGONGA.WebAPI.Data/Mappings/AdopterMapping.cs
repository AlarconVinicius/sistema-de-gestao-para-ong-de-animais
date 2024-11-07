using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGONGA.WebAPI.Business.Models;

namespace SGONGA.WebAPI.Data.Mappings;

public class AdopterMapping : IEntityTypeConfiguration<Adopter>
{
    public void Configure(EntityTypeBuilder<Adopter> builder)
    {
        builder.ToTable("tbl_adopters");

    }
}
