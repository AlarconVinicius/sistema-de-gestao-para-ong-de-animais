using SGONGA.WebAPI.Business.People.Enum;
using System.ComponentModel;

namespace SGONGA.WebAPI.Business.People.Responses;

public class PersonResponse(
    Guid id,
    Guid tenantId,
    string name,
    string nickname,
    EPersonType personType,
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
    string? pixKey,
    DateTime createdAt,
    DateTime updatedAt)
{
    [DisplayName("Id")]
    public Guid Id { get; } = id;

    [DisplayName("Tenant Id")]
    public Guid TenantId { get; } = tenantId;

    [DisplayName("Nome")]
    public string Name { get; } = name;

    [DisplayName("Apelido")]
    public string Nickname { get; } = nickname;

    [DisplayName("Tipo de Usuário")]
    public EPersonType PersonType { get; } = personType;

    [DisplayName("Documento")]
    public string Document { get; } = document;

    [DisplayName("Site")]
    public string Site { get; } = site;

    [DisplayName("E=mail")]
    public string Email { get; } = email;

    [DisplayName("Telefone")]
    public string PhoneNumber { get; } = phoneNumber;

    [DisplayName("Whatsapp Visível")]
    public bool IsPhoneNumberVisible { get; } = isPhoneNumberVisible;

    [DisplayName("Assinar Newsletter")]
    public bool SubscribeToNewsletter { get; } = subscribeToNewsletter;

    [DisplayName("Data de Nascimento")]
    public DateTime BirthDate { get; } = birthDate;

    [DisplayName("Estado")]
    public string State { get; } = state;

    [DisplayName("Cidade")]
    public string City { get; } = city;

    [DisplayName("Sobre")]
    public string? About { get; } = about;

    [DisplayName("Chave Pix")]
    public string? PixKey { get; } = pixKey;

    [DisplayName("Data de Cadastro")]
    public DateTime CreatedAt { get; } = createdAt;

    [DisplayName("Data de Modificação")]
    public DateTime UpdatedAt { get; } = updatedAt;
}
