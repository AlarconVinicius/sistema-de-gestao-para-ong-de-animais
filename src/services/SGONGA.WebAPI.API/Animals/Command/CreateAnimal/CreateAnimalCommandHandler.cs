using SGONGA.WebAPI.API.Abstractions.Messaging;
using SGONGA.WebAPI.API.Animals.Shared;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Interfaces.Repositories;

namespace SGONGA.WebAPI.API.Animals.Command.CreateAnimal;

internal sealed class CreateAnimalCommandHandler(IUnitOfWork UnitOfWork) : ICommandHandler<CreateAnimalCommand>
{
    public async Task<Result> Handle(CreateAnimalCommand command, CancellationToken cancellationToken)
    {
        Result validationResult = RequestValidator.IsValid(command, new CreateAnimalCommandValidator());
        if(validationResult.IsFailed)
            return validationResult.Errors;

        var commandMapped = command.MapCommandToDomain();

        await UnitOfWork.AnimalRepository.AddAsync(commandMapped);

        var result = await UnitOfWork.CommitAsync();

        return result.IsSuccess ? Result.Ok() : result.Errors;
    }
}
