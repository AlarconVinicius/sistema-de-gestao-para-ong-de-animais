﻿using SGONGA.WebAPI.Business.People.Exceptions;
using SGONGA.WebAPI.Business.People.ValueObjects;

namespace SGONGA.WebAPI.Business.Tests.People.ValueObjects;

[Trait("Person", "ValueObjects - Slug")]
public class SlugTests
{

    [Fact]
    public void Constructor_Should_ReturnSlug_WhenCalledWithValidSlug()
    {
        // Arrange
        string validSlug = "Slug válida";

        // Act
        Slug slug = validSlug;

        // Assert
        Assert.Equal("slug-valida", slug);
        Assert.True(slug.UrlPath.Length < Slug.MaxLength);
        Assert.True(slug.UrlPath.Length > Slug.MinLength);
    }

    [Fact]
    public void Constructor_ShouldThrowException_WhenCalledWithInvalidSlug()
    {
        // Arrange
        string invalidSlug = "";

        // Act & Assert
        var exception = Assert.Throws<PersonValidationException>(() =>
        {
            Slug slug = invalidSlug;
        });

        Assert.Equal("Slug inválida", exception.Errors.First().Message);
        Assert.Single(exception.Errors);
    }

    [Theory]
    [InlineData("sl")]
    [InlineData("slug slug slug slug slug slug slug slug slug slug slug slug slug slug slug slug slug slug slug slug slug")]
    public void Constructor_ShouldThrowException_WhenCalledWithInvalidLength(string invalidSlug)
    {
        // Arrange & Act & Assert
        var exception = Assert.Throws<PersonValidationException>(() =>
        {
            Slug slug = invalidSlug;
        });

        Assert.Equal("Slug deve conter de 3 à 100 caracteres", exception.Errors.First().Message);
        Assert.Single(exception.Errors);
    }
}