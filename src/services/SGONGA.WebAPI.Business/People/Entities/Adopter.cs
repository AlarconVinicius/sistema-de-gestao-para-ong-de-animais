using SGONGA.WebAPI.Business.Models.DomainObjects;
using SGONGA.WebAPI.Business.People.Entities;
using SGONGA.WebAPI.Business.People.Enum;

namespace SGONGA.WebAPI.Business.Models;
public sealed class Adopter : Person
{
    private Adopter() { }
    private Adopter(Guid id) : base(id) { }

    public Adopter(Guid id, Guid tenantId, string nome, string apelido, string documento, string site, string email, string telefone, bool telefoneVisivel, bool assinarNewsletter, DateTime dataNascimento, string estado, string cidade, string? sobre) : base(id, tenantId, EUsuarioTipo.Adotante, nome, apelido, documento, site, email, telefone, telefoneVisivel, assinarNewsletter, dataNascimento, estado, cidade, sobre)
    {
    }

    public static Adopter Create(Guid id, Guid tenantId, string nome, string apelido, string documento, string site, string email, string telefone, bool telefoneVisivel, bool assinarNewsletter, DateTime dataNascimento, string estado, string cidade, string? sobre)
    {
        return new Adopter(
            id,
            tenantId,
            nome,
            apelido,
            documento,
            site,
            email,
            telefone,
            telefoneVisivel,
            assinarNewsletter,
            dataNascimento,
            estado,
            cidade,
            sobre);
    }

    public static Adopter Create(Guid id)
    {
        return new Adopter(id);
    }

    public void Update(string nome, string apelido, string site, string email, string telefone, bool telefoneVisivel, string estado, string cidade, string? sobre)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(nome))
            errors.Add("Nome não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(apelido))
            errors.Add("Apelido não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(estado))
            errors.Add("Estado não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(cidade))
            errors.Add("Cidade não pode ser nula ou vazia.");

        if (errors.Count != 0)
            throw new DomainException(string.Join(Environment.NewLine, errors));

        Nome = nome;
        Apelido = apelido;
        Slug = apelido;
        Site = site;
        Email = email;
        Telefone = telefone;
        TelefoneVisivel = telefoneVisivel;
        Estado = estado;
        Cidade = cidade;
        Sobre = sobre;
    }
}
