using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Data.Context;

namespace SGONGA.WebAPI.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ONGDbContext _context;

    private IONGRepository? _ongRepository;
    private IAnimalRepository? _animalRepository;
    private IAdotanteRepository? _adotanteRepository;
    private ISolicitacaoCadastroRepository? _solicitacaoCadastroRepository;

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

    public IAnimalRepository AnimalRepository
    {
        get
        {
            return _animalRepository ??= new AnimalRepository(_context);
        }
    }

    public IAdotanteRepository AdotanteRepository
    {
        get
        {
            return _adotanteRepository ??= new AdotanteRepository(_context);
        }
    }

    public ISolicitacaoCadastroRepository SolicitacaoCadastroRepository
    {
        get
        {
            return _solicitacaoCadastroRepository ??= new SolicitacaoCadastroRepository(_context);
        }
    }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}