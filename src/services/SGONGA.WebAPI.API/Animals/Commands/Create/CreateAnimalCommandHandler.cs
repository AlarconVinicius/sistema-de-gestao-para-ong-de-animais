using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Interfaces.Services;
using SGONGA.WebAPI.Business.Models;

namespace SGONGA.WebAPI.API.Animals.Commands.Create;

internal sealed class CreateAnimalCommandHandler(IONGDbContext Context, ITenantProvider TenantProvider) : ICommandHandler<CreateAnimalCommand>
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

        await Context.Animais.AddAsync(animal, cancellationToken);

        await Context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
