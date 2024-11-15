using SGONGA.WebAPI.Business.Animals.Entities;
using SGONGA.WebAPI.Business.People.Entities;
using SGONGA.WebAPI.Business.People.Exceptions;

namespace SGONGA.WebAPI.Business.Tests.People.Entities;

[Trait("People", "Entities")]
public sealed class OrganizationTests
{
    [Fact]
    public void Constructor_Should_InitializeId_WhenCalledWithValidId()
    {
        // Arrange
        var id = Guid.NewGuid();
        // Act
        var organization = Organization.Create(id);
        // Assert
        Assert.Equal(id, organization.Id);
    }

    [Theory]
    [InlineData("93203054043")]
    [InlineData(null)]
    public void Constructor_Should_InitializeAllProperties_WhenCalledWithValidParameters(string? pixKey)
    {
        // Arrange
        var id = Guid.NewGuid();
        var tenantId = Guid.NewGuid();
        string name = "Org Name";
        string nickname = "OrgNick";
        string document = "93203054043";
        string site = "https://organization.org";
        string email = "contact@organization.org";
        string phoneNumber = "12345678901";
        bool isPhoneNumberVisible = true;
        bool subscribeToNewsletter = true;
        DateTime birthDate = DateTime.Now.AddYears(-20);
        string state = "SP";
        string city = "Sao Paulo";
        string about = "About Org";
        // Act
        var organization = Organization.Create(
            id, tenantId, name, nickname, document, site, email, phoneNumber,
            isPhoneNumberVisible, subscribeToNewsletter, birthDate, state, city, about, pixKey
        );
        // Assert
        Assert.Equal(id, organization.Id);
        Assert.Equal(tenantId, organization.TenantId);
        Assert.Equal(name, organization.Name);
        Assert.Equal(nickname, organization.Nickname);
        Assert.Equal(document, organization.Document);
        Assert.Equal(site, organization.Site);
        Assert.Equal(email, organization.Email);
        Assert.Equal(phoneNumber, organization.PhoneNumber);
        Assert.Equal(isPhoneNumberVisible, organization.IsPhoneNumberVisible);
        Assert.Equal(subscribeToNewsletter, organization.SubscribeToNewsletter);
        Assert.Equal(birthDate, organization.BirthDate);
        Assert.Equal(state, organization.State);
        Assert.Equal(city, organization.City);
        Assert.Equal(about, organization.About);
        Assert.Equal(document, organization.PixKey);
    }

    [Fact]
    public void Constructor_Should_ThrowException_WhenCalledWithInvalidParameters()
    {
        // Arrange
        var id = Guid.NewGuid();
        var tenantId = Guid.NewGuid();
        string name = "";
        string nickname = "";
        string document = "93203054043";
        string site = "https://organization.org";
        string email = "contact@organization.org";
        string phoneNumber = "12345678901";
        bool isPhoneNumberVisible = true;
        bool subscribeToNewsletter = true;
        DateTime birthDate = DateTime.Now.AddYears(-20);
        string state = "";
        string city = "";
        string about = null!;
        string pixKey = null!;

        // Act & Assert
        var exception = Assert.Throws<PersonValidationException>(() =>
            Organization.Create(
                id, tenantId, name, nickname, document, site, email, phoneNumber,
                isPhoneNumberVisible, subscribeToNewsletter, birthDate, state, city, about, pixKey
            )
        );
        Assert.Equal(4, exception.Errors.Length);
    }

    [Fact]
    public void Update_Should_ThrowException_WhenCalledWithInvalidParameters()
    {
        // Arrange
        var id = Guid.NewGuid();
        var tenantId = Guid.NewGuid();
        string name = "Org Name";
        string nickname = "OrgNick";
        string document = "93203054043";
        string site = "https://organization.org";
        string email = "contact@organization.org";
        string phoneNumber = "12345678901";
        bool isPhoneNumberVisible = true;
        bool subscribeToNewsletter = true;
        DateTime birthDate = DateTime.Now.AddYears(-20);
        string state = "SP";
        string city = "Sao Paulo";
        string about = "About Org";
        string pixKey = null!;
        var organization = Organization.Create(
            id, tenantId, name, nickname, document, site, email, phoneNumber,
            isPhoneNumberVisible, subscribeToNewsletter, birthDate, state, city, about, pixKey
        );

        // Act & Assert
        var exception = Assert.Throws<PersonValidationException>(() =>
            organization.Update(
                null!, null!, site, email, phoneNumber,
                isPhoneNumberVisible, null!, null!, about, pixKey
            )
        );
        Assert.Equal(4, exception.Errors.Length);
    }

    [Fact]
    public void Update_ShouldUpdateOrganization_WhenAllFieldsAreValid()
    {
        // Arrange
        var organization = Organization.Create(Guid.NewGuid());
        var name = "Updated Name";
        var nickname = "Updated Nickname";
        var site = "https://newsite.com";
        var email = "newemail@domain.com";
        var phoneNumber = "98765432109";
        var isPhoneNumberVisible = false;
        var state = "RJ";
        var city = "Rio de Janeiro";
        var about = "New about info";
        var pixKey = "pix1234";

        // Act
        organization.Update(name, nickname, site, email, phoneNumber, isPhoneNumberVisible, state, city, about, pixKey);

        // Assert
        Assert.Equal(name, organization.Name);
        Assert.Equal(nickname, organization.Nickname);
        Assert.Equal(site, organization.Site);
        Assert.Equal(email, organization.Email);
        Assert.Equal(phoneNumber, organization.PhoneNumber);
        Assert.Equal(isPhoneNumberVisible, organization.IsPhoneNumberVisible);
        Assert.Equal(state, organization.State);
        Assert.Equal(city, organization.City);
        Assert.Equal(about, organization.About);
        Assert.Equal(pixKey, organization.PixKey);
    }

    [Fact]
    public void ValidateAge_Should_ThrowException_WhenAgeIsLessThan18()
    {
        // Arrange
        var id = Guid.NewGuid();
        var tenantId = Guid.NewGuid();
        DateTime invalidBirthDate = DateTime.Now.AddYears(-17);

        // Act & Assert
        Assert.Throws<PersonValidationException>(() =>
            Organization.Create(
                id, tenantId, "Org Name", "OrgNick", "93203054043", "https://organization.org",
                "contact@organization.org", "12345678901", true, true, invalidBirthDate,
                "SP", "Sao Paulo", "About Org", null
            ));
    }

    [Fact]
    public void ValidateAge_Should_NotThrowException_WhenAgeIsExactly18()
    {
        // Arrange
        var id = Guid.NewGuid();
        var tenantId = Guid.NewGuid();
        DateTime birthDate = new DateTime(DateTime.Now.Year - 18, DateTime.Now.Month, DateTime.Now.Day);

        // Act & Assert
        _ = Organization.Create(
            id, tenantId, "Org Name", "OrgNick", "93203054043", "https://organization.org",
            "contact@organization.org", "12345678901", true, true, birthDate,
            "SP", "Sao Paulo", "About Org", null
            );
    }

    [Fact]
    public void ValidateAge_Should_ThrowException_WhenAgeIs18ButBirthdayHasNotPassedYet()
    {
        // Arrange
        var id = Guid.NewGuid();
        var tenantId = Guid.NewGuid();
        DateTime invalidBirthDate = new DateTime(DateTime.Now.Year - 18, DateTime.Now.Month + 1, DateTime.Now.Day); 

        // Act & Assert
        Assert.Throws<PersonValidationException>(() =>
            Organization.Create(
                id, tenantId, "Org Name", "OrgNick", "93203054043", "https://organization.org",
                "contact@organization.org", "12345678901", true, true, invalidBirthDate,
                "SP", "Sao Paulo", "About Org", null
            ));
    }

    [Fact]
    public void AddAnimal_ShouldThrowException_WhenAnimalIsNull()
    {
        // Arrange
        var id = Guid.NewGuid();
        var tenantId = Guid.NewGuid();
        DateTime birthDate = DateTime.Now.AddYears(-20);
        var organization = Organization.Create(
                id, tenantId, "Org Name", "OrgNick", "93203054043", "https://organization.org",
                "contact@organization.org", "12345678901", true, true, birthDate,
                "SP", "Sao Paulo", "About Org", null
            );

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => organization.AddAnimal(null!));
    }

    [Fact]
    public void AddAnimal_ShouldAddAnimal_WhenAnimalIsValid()
    {
        // Arrange
        var id = Guid.NewGuid();
        var tenantId = Guid.NewGuid();
        DateTime birthDate = DateTime.Now.AddYears(-20);
        var organization = Organization.Create(
                id, tenantId, "Org Name", "OrgNick", "93203054043", "https://organization.org",
                "contact@organization.org", "12345678901", true, true, birthDate,
                "SP", "Sao Paulo", "About Org", null
            );
        var animal = Animal.Create(tenantId, "Rex", "Dog", "Poodle", true, false, "Caramelo", "Médio", "1 à 3 anos", "Dócil", null, "Foto.png", null);

        // Act
        organization.AddAnimal(animal);

        // Assert
        Assert.Contains(animal, organization.Animals);
    }
}
