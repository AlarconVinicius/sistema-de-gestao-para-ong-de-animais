using SGONGA.WebAPI.Business.Models.DomainObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGONGA.WebAPI.Business.Models;

public sealed class Animal : Entity
{
    public Guid TenantId { get; init; }
    public string Nome { get; private set; } = string.Empty;
    public string Especie { get; private set; } = string.Empty;
    public string Raca { get; private set; } = string.Empty;
    public bool Sexo { get; private set; }
    public bool Castrado { get; private set; }
    public string Cor { get; private set; } = string.Empty;
    public string Porte { get; private set; } = string.Empty;
    public string Idade { get; private set; } = string.Empty;
    public string Descricao { get; private set; } = string.Empty;
    public string Observacao { get; private set; } = string.Empty;
    public string ChavePix { get; private set; } = string.Empty;
    public string Foto { get; private set; } = string.Empty;
    //public bool Adotado { get; set; }

    [ForeignKey(nameof(TenantId))]
    public ONG? ONG { get; private set; }

    public Animal() { }

    private Animal(Guid id, Guid tenantId, string nome, string especie, string raca, bool sexo, bool castrado, string cor, string porte, string idade, string descricao, string observacao, string foto, string chavePix = "") : base(id)
    {
        var errors = new List<string>();

        if (tenantId == Guid.Empty)
            errors.Add("TenantId não pode ser vazio.");
        if (string.IsNullOrWhiteSpace(nome))
            errors.Add("Nome não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(especie))
            errors.Add("Espécie não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(raca))
            errors.Add("Raça não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(cor))
            errors.Add("Cor não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(porte))
            errors.Add("Porte não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(descricao))
            errors.Add("Descrição não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(observacao))
            errors.Add("Observação não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(foto))
            errors.Add("Foto não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(idade))
            errors.Add("Idade não pode ser nula ou vazia.");

        if (errors.Any())
            throw new DomainException(string.Join(Environment.NewLine, errors));

        TenantId = tenantId;
        Nome = nome;
        Especie = especie;
        Raca = raca;
        Sexo = sexo;
        Castrado = castrado;
        Cor = cor;
        Porte = porte;
        Idade = idade;
        Descricao = descricao;
        Observacao = observacao;
        ChavePix = chavePix;
        Foto = foto;
    }
    public static Animal Create(Guid tenantId, string nome, string especie, string raca, bool sexo, bool castrado, string cor, string porte, string idade, string descricao, string observacao, string foto, string chavePix = "")
    {
        return new Animal(Guid.NewGuid(), tenantId, nome, especie, raca, sexo, castrado, cor, porte, idade, descricao, observacao, foto, chavePix);
    }

    public void Update(string nome, string especie, string raca, bool sexo, bool castrado, string cor, string porte, string idade, string descricao, string observacao, string foto, string chavePix)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(nome))
            errors.Add("Nome não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(especie))
            errors.Add("Espécie não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(raca))
            errors.Add("Raça não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(cor))
            errors.Add("Cor não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(porte))
            errors.Add("Porte não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(descricao))
            errors.Add("Descrição não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(observacao))
            errors.Add("Observação não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(foto))
            errors.Add("Foto não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(idade))
            errors.Add("Idade não pode ser nula ou vazia.");

        if (errors.Any())
            throw new DomainException(string.Join(Environment.NewLine, errors));

        Nome = nome;
        Especie = especie;
        Raca = raca;
        Sexo = sexo;
        Castrado = castrado;
        Cor = cor;
        Porte = porte;
        Idade = idade;
        Descricao = descricao;
        Observacao = observacao;
        Foto = foto;
        ChavePix = chavePix;
    }
}