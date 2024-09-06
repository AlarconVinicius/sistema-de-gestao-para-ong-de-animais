using SGONGA.WebAPI.Business.Models.DomainObjects;

namespace SGONGA.WebAPI.Business.Models;

public class ONG : Entity
{
    public string Nome { get; private set; } = string.Empty;
    public string Instagram { get; private set; } = string.Empty;
    public string Documento { get; private set; } = string.Empty;
    public Contato Contato { get; private set; } = null!;
    public Endereco Endereco { get; private set; } = null!;
    public string? ChavePix { get; set; } = string.Empty;
    
    public List<Animal> Animais { get; private set; } = new List<Animal>();
    public List<Colaborador> Colaboradores { get; private set; } = new List<Colaborador>();

    public ONG() { }

    public ONG(string nome, string instagram, string documento, Contato contato, Endereco endereco, string? chavePix) : base()
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(instagram))
            throw new ArgumentException("Instagram não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(documento))
            throw new ArgumentException("Documento não pode ser nulo ou vazio.");

        Nome = nome;
        Instagram = instagram;
        Documento = documento;
        ChavePix = chavePix;
        Contato = contato;
        Endereco = endereco;
    }

    public ONG(Guid id, string nome, string instagram, string documento, Contato contato, Endereco endereco, string? chavePix) : base(id)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(instagram))
            throw new ArgumentException("Instagram não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(documento))
            throw new ArgumentException("Documento não pode ser nulo ou vazio.");

        Nome = nome;
        Instagram = instagram;
        Documento = documento;
        ChavePix = chavePix;
        Contato = contato;
        Endereco = endereco;
    }

    public void AddAnimal(Animal animal)
    {
        ArgumentNullException.ThrowIfNull(animal);

        Animais.Add(animal);
    }

    public void AddColaborador(Colaborador colaborador)
    {
        ArgumentNullException.ThrowIfNull(colaborador);

        Colaboradores.Add(colaborador);
    }

    #region Setters
    public void SetNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser nulo ou vazio.");

        Nome = nome;
    }

    public void SetInstagram(string instagram)
    {
        if (string.IsNullOrWhiteSpace(instagram))
            throw new ArgumentException("Instagram não pode ser nulo ou vazio.");

        Instagram = instagram;
    }

    public void SetChavePix(string? chavePix)
    {
        ChavePix = chavePix;
    }

    public void SetContato(Contato contato)
    {
        Contato = contato;
    }

    public void SetEndereco(Endereco endereco)
    {
        Endereco = endereco;
    }
    #endregion
}