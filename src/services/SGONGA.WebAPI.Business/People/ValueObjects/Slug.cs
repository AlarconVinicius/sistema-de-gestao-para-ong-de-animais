﻿using SGONGA.Core.Extensions;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.People.Exceptions;

namespace SGONGA.WebAPI.Business.People.ValueObjects;

public sealed record Slug : ValueObject
{
    #region Constants
    public const short MaxLength = 100;
    public const short MinLength = 3;
    #endregion

    #region Constructors
    private Slug(string urlPath)
    {
        if (string.IsNullOrWhiteSpace(urlPath) || urlPath.Length > MaxLength || urlPath.Length < MinLength)
            throw new PersonValidationException(Error.Validation($"Slug deve conter de {MinLength} à {MaxLength} caracteres"));

        UrlPath = urlPath.SlugifyString();
    }
    #endregion

    #region Properties
    public string UrlPath { get; }
    #endregion

    #region Operators
    public static implicit operator string(Slug slug) => slug.UrlPath;
    public static implicit operator Slug(string urlPath) => new(urlPath);
    #endregion
}