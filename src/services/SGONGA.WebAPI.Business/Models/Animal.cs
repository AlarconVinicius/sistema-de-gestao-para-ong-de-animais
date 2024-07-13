using SGONGA.WebAPI.Business.Models.DomainObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGONGA.WebAPI.Business.Models;

public class Animal : Entity
{
    public Guid TenantId { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Especie { get; private set; } = string.Empty;
    public string Raca { get; private set; } = string.Empty;
    public string Cor { get; private set; } = string.Empty;
    public string Porte { get; private set; } = string.Empty;
    public string Descricao { get; private set; } = string.Empty;
    public string Observacao { get; private set; } = string.Empty;
    public string ChavePix { get; private set; } = string.Empty;
    public List<string> Fotos { get; private set; } = new List<string>();


    [ForeignKey(nameof(TenantId))]
    public virtual ONG? ONG { get; private set; }

    protected Animal() { }

    public Animal(Guid tenantId, string nome, string especie, string raca, string cor, string porte, string descricao, string observacao, List<string> fotos, string chavePix = "") : base()
    {
        if (tenantId == Guid.Empty)
            throw new ArgumentException("TenantId não pode ser vazio.");
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(especie))
            throw new ArgumentException("Espécie não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(raca))
            throw new ArgumentException("Raça não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(cor))
            throw new ArgumentException("Cor não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(porte))
            throw new ArgumentException("Porte não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(observacao))
            throw new ArgumentException("Observação não pode ser nula ou vazia.");
        ValidarFotos(fotos);

        TenantId = tenantId;
        Nome = nome;
        Especie = especie;
        Raca = raca;
        Cor = cor;
        Porte = porte;
        Descricao = descricao;
        Observacao = observacao;
        ChavePix = chavePix;
        Fotos = fotos;
    }

    public Animal(Guid id, Guid tenantId, string nome, string especie, string raca, string cor, string porte, string descricao, string observacao, List<string> fotos, string chavePix = "") : base(id)
    {
        if (tenantId == Guid.Empty)
            throw new ArgumentException("TenantId não pode ser vazio.");
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(especie))
            throw new ArgumentException("Espécie não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(raca))
            throw new ArgumentException("Raça não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(cor))
            throw new ArgumentException("Cor não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(porte))
            throw new ArgumentException("Porte não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(observacao))
            throw new ArgumentException("Observação não pode ser nula ou vazia.");
        ValidarFotos(fotos);

        TenantId = tenantId;
        Nome = nome;
        Especie = especie;
        Raca = raca;
        Cor = cor;
        Porte = porte;
        Descricao = descricao;
        Observacao = observacao;
        ChavePix = chavePix;
        Fotos = fotos;
    }

    private static void ValidarFotos(List<string> fotos)
    {
        if (fotos == null || fotos.Count == 0)
            throw new DomainException("A lista de fotos deve conter pelo menos uma foto.");
    }

    #region Setters
    public void SetNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("Nome não pode ser nulo ou vazio.");
        Nome = nome;
    }

    public void SetEspecie(string especie)
    {
        if (string.IsNullOrWhiteSpace(especie))
            throw new DomainException("Espécie não pode ser nula ou vazia.");
        Especie = especie;
    }

    public void SetRaca(string raca)
    {
        if (string.IsNullOrWhiteSpace(raca))
            throw new DomainException("Raça não pode ser nula ou vazia.");
        Raca = raca;
    }

    public void SetCor(string cor)
    {
        if (string.IsNullOrWhiteSpace(cor))
            throw new DomainException("Cor não pode ser nula ou vazia.");
        Cor = cor;
    }

    public void SetPorte(string porte)
    {
        if (string.IsNullOrWhiteSpace(porte))
            throw new DomainException("Porte não pode ser nulo ou vazio.");
        Porte = porte;
    }

    public void SetDescricao(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new DomainException("Descrição não pode ser nula ou vazia.");
        Descricao = descricao;
    }

    public void SetObservacao(string observacao)
    {
        if (string.IsNullOrWhiteSpace(observacao))
            throw new DomainException("Observação não pode ser nula ou vazia.");
        Observacao = observacao;
    }

    public void SetChavePix(string chavePix)
    {
        if (string.IsNullOrWhiteSpace(chavePix))
            throw new DomainException("Chave Pix não pode ser nula ou vazia.");
        ChavePix = chavePix;
    }

    public void SetFotos(List<string> fotos)
    {
        ValidarFotos(fotos);
        Fotos = fotos;
    }
    #endregion
}