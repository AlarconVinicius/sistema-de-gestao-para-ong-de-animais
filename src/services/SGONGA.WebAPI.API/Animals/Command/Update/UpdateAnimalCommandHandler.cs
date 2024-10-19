using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.API.Animals.Errors;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;

namespace SGONGA.WebAPI.API.Animals.Command.Update;

internal sealed class UpdateAnimalCommandHandler(IUnitOfWork UnitOfWork, TenantProvider TenantProvider) : ICommandHandler<UpdateAnimalCommand>
{
    public async Task<Result> Handle(UpdateAnimalCommand command, CancellationToken cancellationToken)
    {
        Result validationResult = RequestValidator.IsValid(command, new UpdateAnimalCommandValidator());
        if (validationResult.IsFailed)
            return validationResult.Errors;

        var tenantId = TenantProvider.TenantId
            ?? throw new InvalidOperationException("TenantId cannot be null when saving entities with the TenantId property.");

        if (AnimalExiste(command.Id, true).IsFailed)
            return AnimalErrors.AnimalNotFound(command.Id);

        var animal = (await UnitOfWork.AnimalRepository.GetByIdAsync(command.Id)).Value;

        animal.Update(command.Nome, command.Especie, command.Raca, command.Sexo, command.Castrado, command.Cor, command.Porte, command.Idade, command.Descricao, command.Observacao, command.Foto, command.ChavePix);

        UnitOfWork.AnimalRepository.Update(animal);

        var result = await UnitOfWork.CommitAsync();

        return result.IsSuccess ? Result.Ok() : result.Errors;
    }
    private Result AnimalExiste(Guid id, bool tenantFiltro)
    {
        var exists = tenantFiltro
            ? UnitOfWork.AnimalRepository.SearchAsync(f => f.Id == id).Result.Value.Any()
            : UnitOfWork.AnimalRepository.SearchWithoutTenantAsync(f => f.Id == id).Result.Value.Any();

        return exists ? Result.Ok() : Error.NullValue;
    }
}
