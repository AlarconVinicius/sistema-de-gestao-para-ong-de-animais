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
            command.Nome, 
            command.Especie, 
            command.Raca, 
            command.Sexo, 
            command.Castrado, 
            command.Cor, 
            command.Porte, 
            command.Idade, 
            command.Descricao, 
            command.Observacao, 
            command.Foto, 
            command.ChavePix);

        await UnitOfWork.InsertAsync(animal, cancellationToken);

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
