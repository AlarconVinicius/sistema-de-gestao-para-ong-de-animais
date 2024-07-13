using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SGONGA.WebAPI.Business.Models;

namespace SGONGA.WebAPI.Data.Context;

public class ONGDbContext : DbContext
{
    public ONGDbContext(DbContextOptions<ONGDbContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<ONG> ONGs { get; set; }
    public DbSet<Colaborador> Colaboradores { get; set; }
    public DbSet<Animal> Animais { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ONGDbContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        base.OnModelCreating(modelBuilder);
    }
}
