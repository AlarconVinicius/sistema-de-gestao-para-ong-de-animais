using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.People.Enum;
using SGONGA.WebAPI.Business.People.Exceptions;
using SGONGA.WebAPI.Business.People.ValueObjects;
using SGONGA.WebAPI.Business.Shared.Entities;

namespace SGONGA.WebAPI.Business.People.Entities;
public abstract class Person : Entity
{
    public Guid TenantId { get; init; }
    public EPersonType PersonType { get; protected set; }
    public string Name { get; protected set; }
    public string Nickname { get; protected set; }
    public Slug Slug { get; protected set; }
    public Document Document { get; init; }
    public Site Site { get; protected set; }
    public Email Email { get; protected set; }
    public PhoneNumber PhoneNumber { get; protected set; }
    public bool IsPhoneNumberVisible { get; protected set; }
    public bool SubscribeToNewsletter { get; protected set; }
    public DateTime BirthDate { get; init; }
    public string State { get; protected set; }
    public string City { get; protected set; }
    public string? About { get; protected set; }

    protected Person() { }

    protected Person(Guid id) : base(id) { }

    protected Person(Guid id, Guid tenantId, EPersonType personType, string name, string nickname, string document, string site, string email, string phoneNumber, bool isPhoneNumberVisible, bool subscribeToNewsletter, DateTime birthDate, string state, string city, string? about) : base(id)
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

        ValidateAge(birthDate);
        BirthDate = birthDate;
        Site = site;
        Email = email;
        PhoneNumber = phoneNumber;
        Slug = nickname;
        TenantId = tenantId;
        PersonType = personType;
        Name = name;
        Nickname = nickname;
        Document = document;
        IsPhoneNumberVisible = isPhoneNumberVisible;
        SubscribeToNewsletter = subscribeToNewsletter;
        State = state;
        City = city;
        About = about;
    }

    #region Methods
    protected static void ValidateAge(DateTime birthDate)
    {
        DateTime currentDate = DateTime.Now;
        int age = currentDate.Year - birthDate.Year;

        if (birthDate.Date > currentDate.AddYears(-age))
        {
            age--;
        }

        if (age < 18)
        {
            throw new PersonValidationException(Error.Validation("O usuário deve ter mais de 18 anos."));
        }
    }
    #endregion
}
