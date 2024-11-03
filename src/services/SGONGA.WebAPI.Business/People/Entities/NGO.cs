using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Models.DomainObjects;
using SGONGA.WebAPI.Business.People.Enum;

namespace SGONGA.WebAPI.Business.People.Entities;

public sealed class NGO : Person
{
    public string? ChavePix { get; private set; } = string.Empty;
    public List<Animal> Animais { get; private set; } = new();

    private NGO() { }
    private NGO(Guid id) : base(id) { }

    private NGO(Guid id, Guid tenantId, string nome, string apelido, string documento, string site, string email, string telefone, bool telefoneVisivel, bool assinarNewsletter, DateTime dataNascimento, string estado, string cidade, string? sobre, string? chavePix) : base(id, tenantId, EUsuarioTipo.ONG, nome, apelido, documento, site, email, telefone, telefoneVisivel, assinarNewsletter, dataNascimento, estado, cidade, sobre)
    {
        ChavePix = string.IsNullOrEmpty(chavePix) ? documento : chavePix;
    }
    public static NGO Create(Guid id, Guid tenantId, string nome, string apelido, string documento, string site, string email, string telefone, bool telefoneVisivel, bool assinarNewsletter, DateTime dataNascimento, string estado, string cidade, string? sobre, string? chavePix)
    {
        return new NGO(
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
            sobre,
            chavePix);
    }

    public static NGO Create(Guid id)
    {
        return new NGO(id);
    }

    public void Update(string nome, string apelido, string site, string email, string telefone, bool telefoneVisivel, string estado, string cidade, string? sobre, string? chavePix)
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
        ChavePix = chavePix;
    }
    public void AddAnimal(Animal animal)
    {
        ArgumentNullException.ThrowIfNull(animal);

        Animais.Add(animal);
    }

    #region Setters
    public void SetChavePix(string? chavePix)
    {
        ChavePix = chavePix;
    }
    #endregion
}