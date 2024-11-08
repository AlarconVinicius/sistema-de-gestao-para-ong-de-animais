using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.People.Entities;
using SGONGA.WebAPI.Business.People.Enum;
using SGONGA.WebAPI.Business.People.Exceptions;

namespace SGONGA.WebAPI.Business.Models;
public sealed class Adopter : Person
{
    private Adopter() { }
    private Adopter(Guid id) : base(id) { }

    private Adopter(
        Guid id, 
        Guid tenantId, 
        string name, 
        string nickname, 
        string document,
        string site,
        string email, 
        string phoneNumber,
        bool isPhoneNumberVisible,
        bool subscribeToNewsletter, 
        DateTime birthDate,
        string state,
        string city,
        string? about) : base(id, tenantId, EPersonType.Adopter, name, nickname, document, site, email, phoneNumber, isPhoneNumberVisible, subscribeToNewsletter, birthDate, state, city, about)
    {
    }

    public static Adopter Create(Guid id, Guid tenantId, string name, string nickname, string document, string site, string email, string phoneNumber, bool isPhoneNumberVisible, bool subscribeToNewsletter, DateTime birthDate, string state, string city, string? about)
    {
        return new Adopter(
            id,
            tenantId,
            name,
            nickname,
            document,
            site,
            email,
            phoneNumber,
            isPhoneNumberVisible,
            subscribeToNewsletter,
            birthDate,
            state,
            city,
            about);
    }

    public static Adopter Create(Guid id)
    {
        return new Adopter(id);
    }

    public void Update(string name, string nickname, string site, string email, string phoneNumber, bool isPhoneNumberVisible, string state, string city, string? about)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(name))
            errors.Add("Nome não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(nickname))
            errors.Add("Apelido não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(state))
            errors.Add("Estado não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(city))
            errors.Add("Cidade não pode ser nula ou vazia.");

        if (errors.Count != 0)
            throw new PersonValidationException(errors.Select(Error.Validation).ToArray());

        Name = name;
        Nickname = nickname;
        Slug = nickname;
        Site = site;
        Email = email;
        PhoneNumber = phoneNumber;
        IsPhoneNumberVisible = isPhoneNumberVisible;
        State = state;
        City = city;
        About = about;
    }
}
