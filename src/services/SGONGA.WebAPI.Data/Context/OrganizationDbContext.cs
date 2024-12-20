﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SGONGA.WebAPI.Business.Animals.Entities;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.People.Entities;

namespace SGONGA.WebAPI.Data.Context;

public class OrganizationDbContext : DbContext
{
    public OrganizationDbContext(DbContextOptions<OrganizationDbContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Person> People { get; set; }
    public DbSet<Adopter> Adopters { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Animal> Animals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrganizationDbContext).Assembly);

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
            //if (tenantProperty != null && tenantProperty.CurrentValue!.ToString() == tenantEmpty!.ToString() && entry.State == EntityState.Added)
            //{
            //    if (_tenantId.IsFailed) 
            //        throw new InvalidOperationException("O TenantId não pode ser nulo ao salvar entidades com a propriedade TenantId.");

            //    tenantProperty.CurrentValue = _tenantId.Value;
            //}

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
