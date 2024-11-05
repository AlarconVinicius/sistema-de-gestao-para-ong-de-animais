using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.API.Animals.Errors;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Animals.Entities;
using SGONGA.WebAPI.Business.Animals.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Shared.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Tenants.Interfaces.Handlers;

namespace SGONGA.WebAPI.API.Animals.Commands.Delete;

internal sealed class DeleteAnimalCommandHandler(IGenericUnitOfWork UnitOfWork, IAnimalRepository AnimalRepository, ITenantProvider TenantProvider) : ICommandHandler<DeleteAnimalCommand>
{
    public async Task<Result> Handle(DeleteAnimalCommand command, CancellationToken cancellationToken)
    {
        Result validationResult = command.IsValid();
        if (validationResult.IsFailed)
            return validationResult.Errors;

        Result<Guid> tenantId = await TenantProvider.GetTenantId();
        if (tenantId.IsFailed)
            return tenantId.Errors;

        if ((await AnimalExiste(command.Id, tenantId.Value)).IsFailed)
            return AnimalErrors.AnimalNotFound(command.Id);

        UnitOfWork.Delete(new Animal { Id = command.Id });

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
    private async Task<Result> AnimalExiste(Guid id, Guid tenantId)
    {
        return await AnimalRepository.ExistsAsync(q => q.Id == id && q.TenantId == tenantId) 
            ? Result.Ok() 
            : Error.NullValue;
    }
}
