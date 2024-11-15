using SGONGA.WebAPI.Business.People.Exceptions;
using SGONGA.WebAPI.Business.People.ValueObjects;

namespace SGONGA.WebAPI.Business.Tests.People.ValueObjects;

[Trait("People", "ValueObjects")]
public sealed class SiteTests
{
    [Theory]
    [InlineData("https://mywebsite.com")]
    [InlineData("https://www.mywebsite.com")]
    [InlineData("https://mywebsite.com/sub-domain")]
    [InlineData("http://mywebsite.com")]
    [InlineData("http://www.mywebsite.com")]
    [InlineData("http://mywebsite.com/sub-domain")]
    public void Constructor_Should_Initialize_WhenCalledWithValidUrls(string validSite)
    {
        // Arrange & Act
        Site site = validSite;

        // Assert
        Assert.Equal(validSite, site.Url);
        Assert.True(site.Url.Length < Site.MaxLength);
        Assert.True(site.Url.Length > Site.MinLength);
    }

    [Theory]
    [InlineData("mywebsite.com")]
    [InlineData("www.mywebsite.com")]
    [InlineData("mywebsite.com/sub-domain")]
    public void Constructor_Should_ThrowException_WhenCalledWithVInvalidUrls(string invalidSite)
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<PersonValidationException>(() =>
        {
            Site site = invalidSite;
        });

        Assert.Contains(exception.Errors, e => e.Message == "URL inválida. O formato esperado deve começar com 'http://' ou 'https://', seguido de um domínio válido, como 'https://site.com' ou 'http://site.com'.");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("site")]
    [InlineData("site site site site site site site site site site site site site site site site site site site site site site site site site site site site site site site site  site  site  site  site  site  site site site")]
    public void Constructor_Should_ThrowException_WhenCalledWithInvalidLength(string invalidSite)
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<PersonValidationException>(() =>
        {
            Site site = invalidSite;
        });

        Assert.Contains(exception.Errors, e => e.Message == $"URL deve conter entre {Site.MinLength} e {Site.MaxLength} caracteres.");
    }
}
