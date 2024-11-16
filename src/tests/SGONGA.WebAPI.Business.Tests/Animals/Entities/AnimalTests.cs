using SGONGA.WebAPI.Business.Animals.Entities;
using SGONGA.WebAPI.Business.Animals.Exceptions;

namespace SGONGA.WebAPI.Business.Tests.Animals.Entities;

[Trait("Animals", "Entities")]
public class AnimalTests
{
    [Fact(DisplayName = "Constructor initializes with default values when called with no parameters")]
    public void Constructor_Should_InitializeWithDefaultValues_WhenCalledWithNoParameters()
    {
        // Act
        var animal = new Animal();

        // Assert
        Assert.NotNull(animal);
        Assert.Equal(Guid.Empty, animal.TenantId);
        Assert.Null(animal.Name);
        Assert.Null(animal.Species);
        Assert.Null(animal.Breed);
        Assert.False(animal.Gender);
        Assert.False(animal.Neutered);
        Assert.Null(animal.Color);
        Assert.Null(animal.Size);
        Assert.Null(animal.Age);
        Assert.Null(animal.Description);
        Assert.Null(animal.Note);
        Assert.Null(animal.PixKey);
        Assert.Null(animal.Photo);
        Assert.False(animal.Adopted);
        Assert.Null(animal.Organization);
    }

    [Fact(DisplayName = "Create returns animal when called with valid parameters")]
    public void Create_Should_ReturnAnimal_WhenCalledWithValidParameters()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
        string name = "Luna";
        string species = "Dog";
        string breed = "Labrador";
        bool gender = true;
        bool neutered = true;
        string color = "Black";
        string size = "Large";
        string age = "3 years";
        string description = "Friendly and energetic.";
        string photo = "https://example.com/photo.jpg";

        // Act
        var animal = Animal.Create(tenantId, name, species, breed, gender, neutered, color, size, age, description, null, photo, null);

        // Assert
        Assert.NotNull(animal);
        Assert.Equal(tenantId, animal.TenantId);
        Assert.Equal(name, animal.Name);
        Assert.Equal(species, animal.Species);
        Assert.Equal(breed, animal.Breed);
        Assert.Equal(gender, animal.Gender);
        Assert.Equal(neutered, animal.Neutered);
        Assert.Equal(color, animal.Color);
        Assert.Equal(size, animal.Size);
        Assert.Equal(age, animal.Age);
        Assert.Equal(description, animal.Description);
        Assert.Equal(photo, animal.Photo);
    }

    [Fact(DisplayName = "Create throws DomainException when called with invalid parameters")]
    public void Create_Should_ThrowDomainException_WhenCalledWithInvalidParameters()
    {
        // Arrange
        var tenantId = Guid.Empty;
        string name = string.Empty;
        string species = null!;
        string breed = null!;
        bool gender = true;
        bool neutered = false;
        string color = null!;
        string size = string.Empty;
        string age = null!;
        string description = null!;
        string photo = string.Empty;

        // Act & Assert
        var exception = Assert.Throws<AnimalValidationException>(() =>
            Animal.Create(tenantId, name, species, breed, gender, neutered, color, size, age, description, null, photo, null));

        Assert.NotNull(exception);
        Assert.Equal(9, exception.Errors.Length);
    }

    [Fact(DisplayName = "Update updates animal when called with valid parameters")]
    public void Update_Should_UpdateAnimal_WhenCalledWithValidParameters()
    {
        // Arrange
        var animal = Animal.Create(Guid.NewGuid(), "Luna", "Dog", "Labrador", true, true, "Black", "Large", "3 years", "Friendly.", null, "https://example.com/photo.jpg", null);
        string newName = "Bella";
        string newSpecies = "Cat";
        string newBreed = "Siamese";
        bool newGender = false;
        bool newNeutered = true;
        string newColor = "Gray";
        string newSize = "Medium";
        string newAge = "2 years";
        string newDescription = "Calm and affectionate.";
        string newPhoto = "https://example.com/new-photo.jpg";

        // Act
        animal.Update(newName, newSpecies, newBreed, newGender, newNeutered, newColor, newSize, newAge, newDescription, null, newPhoto, null);

        // Assert
        Assert.Equal(newName, animal.Name);
        Assert.Equal(newSpecies, animal.Species);
        Assert.Equal(newBreed, animal.Breed);
        Assert.Equal(newGender, animal.Gender);
        Assert.Equal(newNeutered, animal.Neutered);
        Assert.Equal(newColor, animal.Color);
        Assert.Equal(newSize, animal.Size);
        Assert.Equal(newAge, animal.Age);
        Assert.Equal(newDescription, animal.Description);
        Assert.Equal(newPhoto, animal.Photo);
    }

    [Fact(DisplayName = "Update throws DomainException when called with invalid parameters")]
    public void Update_Should_ThrowDomainException_WhenCalledWithInvalidParameters()
    {
        // Arrange
        var animal = Animal.Create(Guid.NewGuid(), "Luna", "Dog", "Labrador", true, true, "Black", "Large", "3 years", "Friendly.", null, "https://example.com/photo.jpg", null);

        // Act & Assert
        var exception = Assert.Throws<AnimalValidationException>(() =>
            animal.Update(string.Empty, null!, null!, true, true, null!, null!, null!, null!, null, null!, null));

        Assert.NotNull(exception);
        Assert.Equal(8, exception.Errors.Count());
    }
}