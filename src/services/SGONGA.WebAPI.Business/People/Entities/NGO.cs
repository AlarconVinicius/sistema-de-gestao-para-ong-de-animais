using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Animals.Entities;
using SGONGA.WebAPI.Business.People.Enum;
using SGONGA.WebAPI.Business.People.Exceptions;

namespace SGONGA.WebAPI.Business.People.Entities;

public sealed class NGO : Person
{
    public string? PixKey { get; private set; }
    public List<Animal> Animals { get; private set; }

    private NGO() { }
    private NGO(Guid id) : base(id) { }

    private NGO(
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
        string? about,
        string? pixKey) : base(id, tenantId, EPersonType.NGO, name, nickname, document, site, email, phoneNumber, isPhoneNumberVisible, subscribeToNewsletter, birthDate, state, city, about)
    {
        PixKey = string.IsNullOrEmpty(pixKey) ? document : pixKey;
    }
    public static NGO Create(Guid id, Guid tenantId, string name, string nickname, string document, string site, string email, string phoneNumber, bool isPhoneNumberVisible, bool subscribeToNewsletter, DateTime birthDate, string state, string city, string? about, string? pixKey)
    {
        return new NGO(
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
            about,
            pixKey);
    }

    public static NGO Create(Guid id)
    {
        return new NGO(id);
    }

    public void Update(string name, string nickname, string site, string email, string phoneNumber, bool isPhoneNumberVisible, string state, string city, string? about, string? pixKey)
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
        PixKey = pixKey;
    }
    public void AddAnimal(Animal animal)
    {
        ArgumentNullException.ThrowIfNull(animal);

        Animals.Add(animal);
    }

    #region Setters
    public void SetChavePix(string? chavePix)
    {
        PixKey = chavePix;
    }
    #endregion
}