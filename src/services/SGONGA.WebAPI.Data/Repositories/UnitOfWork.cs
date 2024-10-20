using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Data.Context;

namespace SGONGA.WebAPI.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ONGDbContext _context;

    private IONGRepository? _ongRepository;
    private IAdotanteRepository? _adotanteRepository;

    public UnitOfWork(ONGDbContext context)
    {
        _context = context;
    }

    public IONGRepository ONGRepository
    {
        get
        {
            return _ongRepository ??= new ONGRepository(_context);
        }
    }

    public IAdotanteRepository AdotanteRepository
    {
        get
        {
            return _adotanteRepository ??= new AdotanteRepository(_context);
        }
    }

    public async Task<Result<int>> CommitAsync()
    {
        var result = await _context.SaveChangesAsync();
        return result > 0 ? result : Error.CommitFailed;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}