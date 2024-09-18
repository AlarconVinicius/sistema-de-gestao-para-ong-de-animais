using SGONGA.Core.Extensions;
using SGONGA.WebAPI.Business.Models.DomainObjects;
using System.Text.RegularExpressions;

namespace SGONGA.WebAPI.Business.Models;
public abstract class Usuario : Entity
{
    public Guid TenantId { get; private set; }
    public EUsuarioTipo UsuarioTipo { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Apelido { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public string Documento { get; private set; } = string.Empty;
    public string Site { get; private set; } = string.Empty;
    public Contato Contato { get; private set; } = null!;
    public bool TelefoneVisivel { get; private set; } = false;
    public bool AssinarNewsletter { get; private set; } = false;
    public DateTime DataNascimento { get; private set; }
    public string Estado { get; private set; } = string.Empty;
    public string Cidade { get; private set; } = string.Empty;
    public string? Sobre { get; private set; } = string.Empty;

    protected Usuario(){ }

    protected Usuario(Guid id, Guid tenantId, EUsuarioTipo usuarioTipo, string nome, string apelido, string documento, string site, Contato contato, bool telefoneVisivel, bool assinarNewsletter, DateTime dataNascimento, string estado, string cidade, string? sobre) : base(id)
    {
        SetDataNascimento(dataNascimento);
        SetSite(site);
        SetSlug(apelido);
        TenantId = tenantId;
        UsuarioTipo = usuarioTipo;
        Nome = nome;
        Apelido = apelido;
        Documento = documento;
        Contato = contato;
        TelefoneVisivel = telefoneVisivel;
        AssinarNewsletter = assinarNewsletter;
        Estado = estado;
        Cidade = cidade;
        Sobre = sobre;
    }

    #region Validators
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
    public static void ValidarUrl(string url)
    {
        string pattern = @"^(https?:\/\/)?([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}(\/[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+)*\/?$";
        Regex rgx = new(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

        if (!rgx.IsMatch(url))
        {
            throw new ArgumentException("URL inválida.");
        }
    }
    #endregion

    #region Setters
    private void SetSlug(string slug)
    {
        Slug = slug.SlugifyString();
    }
    public void SetNome(string nome)
    {
        Nome = nome;
    }
    public void SetApelido(string apelido)
    {
        Apelido = apelido;
        SetSlug(apelido);
    }
    public void SetContato(Contato contato)
    {
        Contato = contato;
    }
    public void SetEndereco(string estado, string cidade)
    {
        Estado = estado;
        Cidade = cidade;
    }
    public void SetTenant(Guid tenantId)
    {
        TenantId = tenantId;
    }
    public void SetDataNascimento(DateTime dataNascimento)
    {
        ValidarIdade(dataNascimento);
        DataNascimento = dataNascimento;
    }

    public void SetSite(string site)
    {
        ValidarUrl(site);
        Site = site;
    }
    #endregion

}
