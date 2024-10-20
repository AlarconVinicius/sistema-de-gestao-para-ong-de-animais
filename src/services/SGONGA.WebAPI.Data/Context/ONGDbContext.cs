using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;

namespace SGONGA.WebAPI.Data.Context;

public class ONGDbContext : DbContext, IONGDbContext
{
    private readonly Guid? _tenantId;
    public ONGDbContext(DbContextOptions<ONGDbContext> options, TenantProvider tenantProvider) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
        _tenantId = tenantProvider.TenantId;
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Adotante> Adotantes { get; set; }
    public DbSet<ONG> ONGs { get; set; }
    public DbSet<Animal> Animais { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ONGDbContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        //modelBuilder
        //    .Entity<Usuario>()
        //    .HasQueryFilter(x => x.TenantId == _tenantId);

        //modelBuilder
        //    .Entity<Animal>()
        //    .HasQueryFilter(x => x.TenantId == _tenantId);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
        {
            var tenantProperty = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "TenantId");
            var tenantEmpty = Guid.Empty;
            //if (tenantProperty != null && tenantProperty.CurrentValue == null)
            if (tenantProperty != null && tenantProperty.CurrentValue!.ToString() == tenantEmpty!.ToString() && entry.State == EntityState.Added)
            {
                if (_tenantId is null) 
                    throw new InvalidOperationException("TenantId cannot be null when saving entities with the TenantId property.");

                tenantProperty.CurrentValue = _tenantId;
            }

            var createdAtProperty = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "CreatedAt");
            var updatedAtProperty = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "UpdatedAt");

            if (entry.State == EntityState.Added)
            {
                if (createdAtProperty != null)
                    createdAtProperty.CurrentValue = DateTime.UtcNow;

                if (updatedAtProperty != null)
                    updatedAtProperty.CurrentValue = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                if (createdAtProperty != null)
                    createdAtProperty.IsModified = false;

                if (updatedAtProperty != null)
                    updatedAtProperty.CurrentValue = DateTime.UtcNow;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
