using SGONGA.WebAPI.Business.Models.DomainObjects;

namespace SGONGA.WebAPI.Business.Models;

public sealed class ONG : Usuario
{
    public string? ChavePix { get; private set; } = string.Empty;
    public List<Animal> Animais { get; private set; } = new();

    public ONG() { }

    public ONG(Guid id, Guid tenantId, string nome, string apelido, string documento, string site, Contato contato, bool telefoneVisivel, bool assinarNewsletter, DateTime dataNascimento, string estado, string cidade, string? sobre, string? chavePix) : base(id, tenantId, EUsuarioTipo.ONG, nome, apelido, documento, site, contato, telefoneVisivel, assinarNewsletter, dataNascimento, estado, cidade, sobre)
    {
        ChavePix = string.IsNullOrEmpty(chavePix) ? documento : chavePix;
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