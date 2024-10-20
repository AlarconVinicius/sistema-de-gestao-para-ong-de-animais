using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;

namespace SGONGA.WebAPI.API.Animals.Command.Create;

internal sealed class CreateAnimalCommandHandler(IONGDbContext Context, TenantProvider TenantProvider) : ICommandHandler<CreateAnimalCommand>
{
    public async Task<Result> Handle(CreateAnimalCommand command, CancellationToken cancellationToken)
    {
        var tenantId = TenantProvider.TenantId;
        if (tenantId is null)
            throw new InvalidOperationException("TenantId cannot be null when saving entities with the TenantId property.");

        Result validationResult = RequestValidator.IsValid(command, new CreateAnimalCommandValidator());
        if(validationResult.IsFailed)
            return validationResult.Errors;

        Animal animal = Animal.Create(
            (Guid)tenantId, 
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
