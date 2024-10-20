using Microsoft.EntityFrameworkCore;
using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.API.Animals.Errors;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;

namespace SGONGA.WebAPI.API.Animals.Command.Update;

internal sealed class UpdateAnimalCommandHandler(IONGDbContext Context, TenantProvider TenantProvider) : ICommandHandler<UpdateAnimalCommand>
{
    public async Task<Result> Handle(UpdateAnimalCommand command, CancellationToken cancellationToken)
    {
        Result validationResult = RequestValidator.IsValid(command, new UpdateAnimalCommandValidator());
        if (validationResult.IsFailed)
            return validationResult.Errors;

        var tenantId = TenantProvider.TenantId
            ?? throw new InvalidOperationException("TenantId cannot be null when saving entities with the TenantId property.");

        var animal = await Context.Animais
            .AsNoTracking()
            .Where(q => q.Id == command.Id && q.TenantId == tenantId)
            .FirstOrDefaultAsync(cancellationToken);

        if(animal is null)
            return AnimalErrors.AnimalNotFound(command.Id);


        animal.Update(
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

        Context.Animais.Update(animal);

        await Context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
