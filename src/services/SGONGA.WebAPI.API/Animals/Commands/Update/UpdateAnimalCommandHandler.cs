using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Animals.Entities;
using SGONGA.WebAPI.Business.Animals.Errors;
using SGONGA.WebAPI.Business.Animals.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Shared.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Tenants.Interfaces.Handlers;

namespace SGONGA.WebAPI.API.Animals.Commands.Update;

internal sealed class UpdateAnimalCommandHandler(IGenericUnitOfWork UnitOfWork, IAnimalRepository AnimalRepository, ITenantProvider TenantProvider) : ICommandHandler<UpdateAnimalCommand>
{
    public async Task<Result> Handle(UpdateAnimalCommand command, CancellationToken cancellationToken)
    {
        Result validationResult = command.IsValid();
        if (validationResult.IsFailed)
            return validationResult.Errors;

        Result<Guid> tenantId = await TenantProvider.GetTenantId();
        if (tenantId.IsFailed)
            return tenantId.Errors;

        Animal animal = await AnimalRepository.SearchAsync(q => q.Id == command.Id && q.TenantId == tenantId.Value, cancellationToken);

        if(animal is null)
            return AnimalErrors.AnimalNotFound(command.Id);


        animal.Update(
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

        UnitOfWork.Update(animal);

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
