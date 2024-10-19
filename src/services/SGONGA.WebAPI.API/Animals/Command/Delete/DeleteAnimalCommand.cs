using SGONGA.WebAPI.API.Abstractions.Messaging;

namespace SGONGA.WebAPI.API.Animals.Command.Delete;

public sealed record DeleteAnimalCommand(Guid Id) : ICommand;