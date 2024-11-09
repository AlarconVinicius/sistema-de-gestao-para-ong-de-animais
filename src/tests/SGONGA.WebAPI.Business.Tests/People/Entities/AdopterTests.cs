using SGONGA.WebAPI.Business.People.Entities;
using SGONGA.WebAPI.Business.People.Exceptions;

namespace SGONGA.WebAPI.Business.Tests.People.Entities;

[Trait("Person", "Entities - Adopter")]
public class AdopterTests
{
    [Fact]
    public void Constructor_Should_InitializeId_WhenCalledWithValidId()
    {
        // Arrange
        var id = Guid.NewGuid();
        // Act
        var adopter = Adopter.Create(id);
        // Assert
        Assert.Equal(id, adopter.Id);
    }

    [Fact]
    public void Constructor_Should_InitializeAllProperties_WhenCalledWithValidParameters()
    {
        // Arrange
        var id = Guid.NewGuid();
        var tenantId = Guid.NewGuid();
        string name = "Adopter Name";
        string nickname = "Adopter Nick";
        string document = "93203054043";
        string site = "https://adopter.org";
        string email = "contact@adopter.org";
        string phoneNumber = "12345678901";
        bool isPhoneNumberVisible = true;
        bool subscribeToNewsletter = true;
        DateTime birthDate = DateTime.Now.AddYears(-20);
        string state = "SP";
        string city = "Sao Paulo";
        string about = "About Org";
        // Act
        var adopter = Adopter.Create(
            id, tenantId, name, nickname, document, site, email, phoneNumber,
            isPhoneNumberVisible, subscribeToNewsletter, birthDate, state, city, about
        );
        // Assert
        Assert.Equal(id, adopter.Id);
        Assert.Equal(tenantId, adopter.TenantId);
        Assert.Equal(name, adopter.Name);
        Assert.Equal(nickname, adopter.Nickname);
        Assert.Equal(document, adopter.Document);
        Assert.Equal(site, adopter.Site);
        Assert.Equal(email, adopter.Email);
        Assert.Equal(phoneNumber, adopter.PhoneNumber);
        Assert.Equal(isPhoneNumberVisible, adopter.IsPhoneNumberVisible);
        Assert.Equal(subscribeToNewsletter, adopter.SubscribeToNewsletter);
        Assert.Equal(birthDate, adopter.BirthDate);
        Assert.Equal(state, adopter.State);
        Assert.Equal(city, adopter.City);
        Assert.Equal(about, adopter.About);
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
        string site = "https://adopter.org";
        string email = "contact@adopter.org";
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
            Adopter.Create(
                id, tenantId, name, nickname, document, site, email, phoneNumber,
                isPhoneNumberVisible, subscribeToNewsletter, birthDate, state, city, about
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
        string site = "https://adopter.org";
        string email = "contact@adopter.org";
        string phoneNumber = "12345678901";
        bool isPhoneNumberVisible = true;
        bool subscribeToNewsletter = true;
        DateTime birthDate = DateTime.Now.AddYears(-20);
        string state = "SP";
        string city = "Sao Paulo";
        string about = "About Org";
        var adopter = Adopter.Create(
            id, tenantId, name, nickname, document, site, email, phoneNumber,
            isPhoneNumberVisible, subscribeToNewsletter, birthDate, state, city, about
        );

        // Act & Assert
        var exception = Assert.Throws<PersonValidationException>(() =>
            adopter.Update(
                null!, null!, site, email, phoneNumber,
                isPhoneNumberVisible, null!, null!, about
            )
        );
        Assert.Equal(4, exception.Errors.Length);
    }

    [Fact]
    public void Update_ShouldUpdateAdopter_WhenAllFieldsAreValid()
    {
        // Arrange
        var adopter = Adopter.Create(Guid.NewGuid());
        var name = "Updated Name";
        var nickname = "Updated Nickname";
        var site = "https://newsite.com";
        var email = "newemail@domain.com";
        var phoneNumber = "98765432109";
        var isPhoneNumberVisible = false;
        var state = "RJ";
        var city = "Rio de Janeiro";
        var about = "New about info";

        // Act
        adopter.Update(name, nickname, site, email, phoneNumber, isPhoneNumberVisible, state, city, about);

        // Assert
        Assert.Equal(name, adopter.Name);
        Assert.Equal(nickname, adopter.Nickname);
        Assert.Equal(site, adopter.Site);
        Assert.Equal(email, adopter.Email);
        Assert.Equal(phoneNumber, adopter.PhoneNumber);
        Assert.Equal(isPhoneNumberVisible, adopter.IsPhoneNumberVisible);
        Assert.Equal(state, adopter.State);
        Assert.Equal(city, adopter.City);
        Assert.Equal(about, adopter.About);
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
            Adopter.Create(
                id, tenantId, "Org Name", "OrgNick", "93203054043", "https://adopter.org",
                "contact@adopter.org", "12345678901", true, true, invalidBirthDate,
                "SP", "Sao Paulo", "About Org"
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
        _ = Adopter.Create(
            id, tenantId, "Org Name", "OrgNick", "93203054043", "https://adopter.org",
            "contact@adopter.org", "12345678901", true, true, birthDate,
            "SP", "Sao Paulo", "About Org"
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
            Adopter.Create(
                id, tenantId, "Org Name", "OrgNick", "93203054043", "https://adopter.org",
                "contact@adopter.org", "12345678901", true, true, invalidBirthDate,
                "SP", "Sao Paulo", "About Org"
            ));
    }
}
