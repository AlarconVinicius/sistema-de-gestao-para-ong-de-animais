using SGONGA.WebAPI.Business.Models.DomainObjects;

namespace SGONGA.WebAPI.Business.Models;

public sealed class ONG : Usuario
{
    public string? ChavePix { get; private set; } = string.Empty;
    public List<Animal> Animais { get; private set; } = new();

    private ONG() { }
    private ONG(Guid id) : base(id) { }

    private ONG(Guid id, Guid tenantId, string nome, string apelido, string documento, string site, Contato contato, bool telefoneVisivel, bool assinarNewsletter, DateTime dataNascimento, string estado, string cidade, string? sobre, string? chavePix) : base(id, tenantId, EUsuarioTipo.ONG, nome, apelido, documento, site, contato, telefoneVisivel, assinarNewsletter, dataNascimento, estado, cidade, sobre)
    {
        ChavePix = string.IsNullOrEmpty(chavePix) ? documento : chavePix;
    }
    public static ONG Create(Guid id, Guid tenantId, string nome, string apelido, string documento, string site, Contato contato, bool telefoneVisivel, bool assinarNewsletter, DateTime dataNascimento, string estado, string cidade, string? sobre, string? chavePix)
    {
        return new ONG(
            id,
            tenantId,
            nome,
            apelido,
            documento,
            site,
            contato,
            telefoneVisivel,
            assinarNewsletter,
            dataNascimento,
            estado,
            cidade,
            sobre,
            chavePix);
    }

    public static ONG Create(Guid id)
    {
        return new ONG(id);
    }

    public void Update(string nome, string apelido, string site, Contato contato, bool telefoneVisivel, string estado, string cidade, string? sobre, string? chavePix)
    {
        var errors = new List<string>();

        // Validações
        if (string.IsNullOrWhiteSpace(nome))
            errors.Add("Nome não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(apelido))
            errors.Add("Apelido não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(site))
            errors.Add("Site não pode ser nulo ou vazio.");
        if (contato == null)
            errors.Add("Contato deve conter telefone ou email.");
        if (string.IsNullOrWhiteSpace(estado))
            errors.Add("Estado não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(cidade))
            errors.Add("Cidade não pode ser nula ou vazia.");

        if (errors.Count != 0)
            throw new DomainException(string.Join(Environment.NewLine, errors));

        Nome = nome;
        Apelido = apelido;
        Site = site;
        Contato = contato!;
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