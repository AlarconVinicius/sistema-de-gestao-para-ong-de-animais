﻿using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.Business.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    IONGRepository ONGRepository { get; }
    IAnimalRepository AnimalRepository { get; }
    IAdotanteRepository AdotanteRepository { get; }
    Task<Result<int>> CommitAsync();
}
