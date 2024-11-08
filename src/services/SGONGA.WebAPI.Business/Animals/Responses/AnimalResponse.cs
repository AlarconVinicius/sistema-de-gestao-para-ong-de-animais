namespace SGONGA.WebAPI.Business.Animals.Responses;

public record AnimalResponse(
    Guid Id,
    Guid OrganizationId,
    string Name,
    string Species,
    string Breed,
    bool Gender,
    bool Neutered,
    string Color,
    string Size,
    string Age,
    string OrganizationName,
    string State,
    string City,
    string Description,
    string? Note,
    string? PixKey,
    string Photo,
    DateTime CreatedAt,
    DateTime UpdatedAt);