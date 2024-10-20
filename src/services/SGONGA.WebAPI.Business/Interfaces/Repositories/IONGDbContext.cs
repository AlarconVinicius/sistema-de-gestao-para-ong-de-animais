using Microsoft.EntityFrameworkCore;
using SGONGA.WebAPI.Business.Models;

namespace SGONGA.WebAPI.Business.Interfaces.Repositories;
public interface IONGDbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Adotante> Adotantes { get; set; }
    public DbSet<ONG> ONGs { get; set; }
    public DbSet<Animal> Animais { get; set; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
