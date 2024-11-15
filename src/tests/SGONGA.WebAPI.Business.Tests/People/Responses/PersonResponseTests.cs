using SGONGA.WebAPI.Business.People.Enum;
using SGONGA.WebAPI.Business.People.Responses;

namespace SGONGA.WebAPI.Business.Tests.People.Responses;

[Trait("People", "Responses")]
public sealed class PersonResponseTests
{
    [Fact]
    public void PersonResponse_Should_Initialize_Properties_Correctly()
    {
        // Arrange
        var id = Guid.NewGuid();
        var tenantId = Guid.NewGuid();
        var name = "John Doe";
        var nickname = "John";
        var personType = EPersonType.Organization;
        var document = "12345678901";
        var site = "http://example.com";
        var email = "johndoe@example.com";
        var phoneNumber = "1234567890";
        var isPhoneNumberVisible = true;
        var subscribeToNewsletter = false;
        var birthDate = new DateTime(1990, 1, 1);
        var state = "California";
        var city = "Los Angeles";
        var about = "Lorem ipsum";
        var pixKey = "1234567890";
        var createdAt = DateTime.UtcNow;
        var updatedAt = DateTime.UtcNow;

        // Act
        var personResponse = new PersonResponse(
            id, tenantId, name, nickname, personType, document, site, email, phoneNumber,
            isPhoneNumberVisible, subscribeToNewsletter, birthDate, state, city, about,
            pixKey, createdAt, updatedAt);

        // Assert
        Assert.Equal(id, personResponse.Id);
        Assert.Equal(tenantId, personResponse.TenantId);
        Assert.Equal(name, personResponse.Name);
        Assert.Equal(nickname, personResponse.Nickname);
        Assert.Equal(personType, personResponse.PersonType);
        Assert.Equal(document, personResponse.Document);
        Assert.Equal(site, personResponse.Site);
        Assert.Equal(email, personResponse.Email);
        Assert.Equal(phoneNumber, personResponse.PhoneNumber);
        Assert.Equal(isPhoneNumberVisible, personResponse.IsPhoneNumberVisible);
        Assert.Equal(subscribeToNewsletter, personResponse.SubscribeToNewsletter);
        Assert.Equal(birthDate, personResponse.BirthDate);
        Assert.Equal(state, personResponse.State);
        Assert.Equal(city, personResponse.City);
        Assert.Equal(about, personResponse.About);
        Assert.Equal(pixKey, personResponse.PixKey);
        Assert.Equal(createdAt, personResponse.CreatedAt);
        Assert.Equal(updatedAt, personResponse.UpdatedAt);
    }
}
