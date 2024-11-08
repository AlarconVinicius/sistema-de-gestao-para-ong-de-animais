namespace SGONGA.WebAPI.API.Animals.Commands.Update;

public sealed record UpdateAnimalRequest(
    string Name,
    string Species,
    string Breed,
    bool Gender,
    bool Neutered,
    string Color,
    string Size,
    string Age,
    string Description,
    string? Note,
    string Photo,
    string? PixKey);