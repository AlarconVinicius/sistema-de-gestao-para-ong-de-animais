namespace SGONGA.WebAPI.Business.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    IONGRepository ONGRepository { get; }
    IAnimalRepository AnimalRepository { get; }
    IAdotanteRepository AdotanteRepository { get; }
    Task<int> CommitAsync();
}
