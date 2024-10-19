using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.API.Animals.Errors;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;

namespace SGONGA.WebAPI.API.Animals.Command.Delete;

internal sealed class DeleteAnimalCommandHandler(IUnitOfWork UnitOfWork, TenantProvider TenantProvider) : ICommandHandler<DeleteAnimalCommand>
{
    public async Task<Result> Handle(DeleteAnimalCommand command, CancellationToken cancellationToken)
    {
        Result validationResult = RequestValidator.IsValid(command, new DeleteAnimalCommandValidator());
        if (validationResult.IsFailed)
            return validationResult.Errors;

        var tenantId = TenantProvider.TenantId
            ?? throw new InvalidOperationException("TenantId cannot be null when saving entities with the TenantId property.");

        if (AnimalExiste(command.Id, true).IsFailed)
            return AnimalErrors.AnimalNotFound(command.Id);

        UnitOfWork.AnimalRepository.Delete(command.Id);

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
