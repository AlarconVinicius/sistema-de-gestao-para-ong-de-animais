namespace SGONGA.WebAPI.Business.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    IONGRepository ONGRepository { get; }
    IAnimalRepository AnimalRepository { get; }
    IAdotanteRepository AdotanteRepository { get; }
    ISolicitacaoCadastroRepository SolicitacaoCadastroRepository { get; }
    Task<int> CommitAsync();
}
