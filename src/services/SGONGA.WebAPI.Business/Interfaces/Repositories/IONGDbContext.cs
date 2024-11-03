using Microsoft.EntityFrameworkCore;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.People.Entities;

namespace SGONGA.WebAPI.Business.Interfaces.Repositories;
public interface IONGDbContext
{
    public DbSet<Person> Usuarios { get; set; }
    public DbSet<Adopter> Adotantes { get; set; }
    public DbSet<NGO> ONGs { get; set; }
    public DbSet<Animal> Animais { get; set; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
