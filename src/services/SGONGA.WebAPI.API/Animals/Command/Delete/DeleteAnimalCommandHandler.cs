using Microsoft.EntityFrameworkCore;
using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.API.Animals.Errors;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;

namespace SGONGA.WebAPI.API.Animals.Command.Delete;

internal sealed class DeleteAnimalCommandHandler(IONGDbContext Context, TenantProvider TenantProvider) : ICommandHandler<DeleteAnimalCommand>
{
    public async Task<Result> Handle(DeleteAnimalCommand command, CancellationToken cancellationToken)
    {
        Result validationResult = RequestValidator.IsValid(command, new DeleteAnimalCommandValidator());
        if (validationResult.IsFailed)
            return validationResult.Errors;

        var tenantId = TenantProvider.TenantId
            ?? throw new InvalidOperationException("TenantId cannot be null when saving entities with the TenantId property.");

        if ((await AnimalExiste(command.Id, tenantId)).IsFailed)
            return AnimalErrors.AnimalNotFound(command.Id);

        Context.Animais.Remove(new Animal { Id = command.Id });

        await Context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
    private async Task<Result> AnimalExiste(Guid id, Guid tenantId)
    {
        return await Context.Animais.AnyAsync(q => q.Id == id && q.TenantId == tenantId) 
            ? Result.Ok() 
            : Error.NullValue;
    }
}
