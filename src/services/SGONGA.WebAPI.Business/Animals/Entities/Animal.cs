using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Animals.Exceptions;
using SGONGA.WebAPI.Business.People.Entities;
using SGONGA.WebAPI.Business.Shared.Entities;

namespace SGONGA.WebAPI.Business.Animals.Entities;

public sealed class Animal : Entity
{
    public Guid TenantId { get; init; }
    public string Name { get; private set; }
    public string Species { get; private set; }
    public string Breed { get; private set; }
    public bool Gender { get; private set; }
    public bool Neutered { get; private set; }
    public string Color { get; private set; }
    public string Size { get; private set; }
    public string Age { get; private set; }
    public string Description { get; private set; }
    public string? Note { get; private set; }
    public string? PixKey { get; private set; }
    public string Photo { get; private set; }
    public bool Adopted { get; private set; }

    public Organization Organization { get; private set; }

    public Animal() { }

    private Animal(Guid id, Guid tenantId, string name, string species, string breed, bool gender, bool neutered, string color, string size, string age, string description, string? note, string photo, string? pixKey) : base(id)
    {
        var errors = new List<string>();

        if (tenantId == Guid.Empty)
            errors.Add("TenantId não pode ser vazio.");
        if (string.IsNullOrWhiteSpace(name))
            errors.Add("Nome não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(species))
            errors.Add("Espécie não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(breed))
            errors.Add("Raça não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(color))
            errors.Add("Cor não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(size))
            errors.Add("Porte não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(description))
            errors.Add("Descrição não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(photo))
            errors.Add("Foto não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(age))
            errors.Add("Idade não pode ser nula ou vazia.");

        if (errors.Count != 0)
            throw new AnimalValidationException(errors.Select(Error.Validation).ToArray());

        TenantId = tenantId;
        Name = name;
        Species = species;
        Breed = breed;
        Gender = gender;
        Neutered = neutered;
        Color = color;
        Size = size;
        Age = age;
        Description = description;
        Note = note;
        PixKey = pixKey;
        Photo = photo;
    }
    public static Animal Create(Guid tenantId, string name, string species, string breed, bool gender, bool neutered, string color, string size, string age, string description, string? note, string photo, string? pixKey)
    {
        return new Animal(Guid.NewGuid(), tenantId, name, species, breed, gender, neutered, color, size, age, description, note, photo, pixKey);
    }

    public void Update(string name, string species, string breed, bool gender, bool neutered, string color, string size, string age, string description, string? note, string photo, string? pixKey)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(name))
            errors.Add("Nome não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(species))
            errors.Add("Espécie não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(breed))
            errors.Add("Raça não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(color))
            errors.Add("Cor não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(size))
            errors.Add("Porte não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(description))
            errors.Add("Descrição não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(photo))
            errors.Add("Foto não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(age))
            errors.Add("Idade não pode ser nula ou vazia.");

        if (errors.Count != 0)
            throw new AnimalValidationException(errors.Select(Error.Validation).ToArray());

        Name = name;
        Species = species;
        Breed = breed;
        Gender = gender;
        Neutered = neutered;
        Color = color;
        Size = size;
        Age = age;
        Description = description;
        Note = note;
        Photo = photo;
        PixKey = pixKey;
    }
}