using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.People.Enum;
using SGONGA.WebAPI.Business.People.ValueObjects;

namespace SGONGA.WebAPI.Business.People.Entities;
public abstract class Person : Entity
{
    public Guid TenantId { get; init; }
    public EUsuarioTipo UsuarioTipo { get; protected set; }
    public string Nome { get; protected set; } = string.Empty;
    public string Apelido { get; protected set; } = string.Empty;
    public Slug Slug { get; protected set; } = string.Empty;
    public string Documento { get; init; } = string.Empty;
    public Site Site { get; protected set; } = string.Empty;
    public Email Email { get; protected set; } = string.Empty;
    public PhoneNumber Telefone { get; protected set; } = string.Empty;
    public bool TelefoneVisivel { get; protected set; } = false;
    public bool AssinarNewsletter { get; protected set; } = false;
    public DateTime DataNascimento { get; init; }
    public string Estado { get; protected set; } = string.Empty;
    public string Cidade { get; protected set; } = string.Empty;
    public string? Sobre { get; protected set; } = string.Empty;

    protected Person() { }

    protected Person(Guid id) : base(id) { }

    protected Person(Guid id, Guid tenantId, EUsuarioTipo usuarioTipo, string nome, string apelido, string documento, string site, string email, string telefone, bool telefoneVisivel, bool assinarNewsletter, DateTime dataNascimento, string estado, string cidade, string? sobre) : base(id)
    {
        ValidarIdade(dataNascimento);
        DataNascimento = dataNascimento;
        Site = site;
        Email = email;
        Telefone = telefone;
        Slug = apelido;
        TenantId = tenantId;
        UsuarioTipo = usuarioTipo;
        Nome = nome;
        Apelido = apelido;
        Documento = documento;
        TelefoneVisivel = telefoneVisivel;
        AssinarNewsletter = assinarNewsletter;
        Estado = estado;
        Cidade = cidade;
        Sobre = sobre;
    }

    #region Methods
    public static void ValidarIdade(DateTime dataNascimento)
    {
        DateTime dataAtual = DateTime.Now;
        int idade = dataAtual.Year - dataNascimento.Year;

        if (dataNascimento.Date > dataAtual.AddYears(-idade))
        {
            idade--;
        }

        if (idade < 18)
        {
            throw new ArgumentException("O usuário deve ter mais de 18 anos.");
        }
    }
    #endregion
}
