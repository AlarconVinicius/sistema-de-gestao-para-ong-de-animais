using SGONGA.WebAPI.Business.Models.DomainObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGONGA.WebAPI.Business.Models;

public class Animal : Entity
{
    public Guid TenantId { get; private set; }
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
    public virtual ONG? ONG { get; private set; }

    public Animal() { }

    public Animal(Guid id, Guid tenantId, string nome, string especie, string raca, bool sexo, bool castrado, string cor, string porte, string idade, string descricao, string observacao, string foto, string chavePix = "") : base(id)
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
        if (string.IsNullOrWhiteSpace(foto))
            throw new ArgumentException("Foto não pode ser nula ou vazia.");
        if (string.IsNullOrWhiteSpace(idade))
            throw new ArgumentException("Idade não pode ser nula ou vazia.");

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
        SetNome(nome);
        SetEspecie(especie);
        SetRaca(raca);
        SetSexo(sexo);
        SetCastrado(castrado);
        SetCor(cor);
        SetPorte(porte);
        SetIdade(idade);
        SetDescricao(descricao);
        SetObservacao(observacao);
        SetFoto(foto);
        SetChavePix(chavePix);
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
    public void SetSexo(bool sexo)
    {
        Sexo = sexo;
    }
    public void SetCastrado(bool castrado)
    {
        Castrado = castrado;
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
    public void SetIdade(string idade)
    {
        if (string.IsNullOrWhiteSpace(idade))
            throw new DomainException("Idade não pode ser nulo ou vazio.");
        Idade = idade;
    }
    public void SetDescricao(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new DomainException("Descrição não pode ser nula ou vazia.");
        Descricao = descricao;
    }
    public void SetObservacao(string observacao)
    {
        Observacao = observacao;
    }
    public void SetChavePix(string chavePix)
    {
        ChavePix = chavePix;
    }

    public void SetFoto(string foto)
    {
        Foto = foto;
    }
    #endregion
}