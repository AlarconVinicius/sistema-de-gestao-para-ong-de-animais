using SGONGA.WebAPI.Business.Models.DomainObjects;

namespace SGONGA.WebAPI.Business.Models;

public class ONG : Entity
{
    public string Nome { get; private set; } = string.Empty;
    public string Descricao { get; private set; } = string.Empty;
    public Contato Contato { get; private set; } = null!;
    public Endereco Endereco { get; private set; } = null!;
    public string ChavePix { get; set; } = string.Empty;
    
    public List<Animal> Animais { get; private set; } = new List<Animal>();
    public List<Colaborador> Colaboradores { get; private set; } = new List<Colaborador>();

    protected ONG() { }

    public ONG(string nome, string descricao, string telefone, string email, string chavePix = "") : base()
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição não pode ser nula ou vazia.");

        Nome = nome;
        Descricao = descricao;
        ChavePix = chavePix;
        Contato = new Contato(telefone, email);
    }

    public ONG(Guid id, string nome, string descricao, string telefone, string email, string chavePix = "") : base(id)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição não pode ser nula ou vazia.");

        Nome = nome;
        Descricao = descricao;
        ChavePix = chavePix;
        Contato = new Contato(telefone, email);
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

    public void SetDescricao(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição não pode ser nula ou vazia.");

        Descricao = descricao;
    }

    public void SetChavePix(string chavePix)
    {
        if (string.IsNullOrWhiteSpace(chavePix))
            throw new ArgumentException("Chave Pix não pode ser nula ou vazia.");

        ChavePix = chavePix;
    }

    public void SetContato(string telefone, string email)
    {
        if (string.IsNullOrWhiteSpace(telefone) || string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Telefone e Email não podem ser nulos ou vazios.");

        Contato = new Contato(telefone, email);
    }

    public void SetEndereco(string rua, string cidade, string estado, string cep, string complemento = "")
    {
        if (string.IsNullOrWhiteSpace(rua) || string.IsNullOrWhiteSpace(cidade) || string.IsNullOrWhiteSpace(estado) || string.IsNullOrWhiteSpace(cep))
            throw new ArgumentException("Rua, Cidade, Estado e CEP não podem ser nulos ou vazios.");

        Endereco = new Endereco(rua, cidade, estado, cep, complemento);
    }
    #endregion
}