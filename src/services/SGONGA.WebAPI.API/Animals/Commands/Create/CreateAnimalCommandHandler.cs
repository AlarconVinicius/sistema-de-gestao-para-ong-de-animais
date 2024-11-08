using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Animals.Entities;
using SGONGA.WebAPI.Business.Shared.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Tenants.Interfaces.Handlers;

namespace SGONGA.WebAPI.API.Animals.Commands.Create;

internal sealed class CreateAnimalCommandHandler(IGenericUnitOfWork UnitOfWork, ITenantProvider TenantProvider) : ICommandHandler<CreateAnimalCommand>
{
    public async Task<Result> Handle(CreateAnimalCommand command, CancellationToken cancellationToken)
    {
        Result validationResult = command.IsValid();
        if(validationResult.IsFailed)
        {
            return validationResult.Errors;
        }

        Result<Guid> tenantId = await TenantProvider.GetTenantId();
        if (tenantId.IsFailed)
            return tenantId.Errors;

        Animal animal = Animal.Create(
            tenantId.Value, 
            command.Name, 
            command.Species, 
            command.Breed, 
            command.Gender, 
            command.Neutered, 
            command.Color, 
            command.Size, 
            command.Age, 
            command.Description, 
            command.Note, 
            command.Photo, 
            command.PixKey);

        await UnitOfWork.InsertAsync(animal, cancellationToken);

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
