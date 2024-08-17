using SGONGA.WebAPI.Business.Models.DomainObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGONGA.WebAPI.Business.Models;

public class Colaborador : Entity
{
    public Guid TenantId { get; private set; }

    public string Nome { get; private set; } = string.Empty;
    public string Documento { get; private set; } = string.Empty;
    public Contato Contato { get; private set; } = null!;
    public DateTime DataNascimento { get; private set; }


    [ForeignKey(nameof(TenantId))]
    public virtual ONG? ONG { get; private set; }

    public Colaborador() { }

    public Colaborador(Guid id, string nome, string documento, Contato contato, DateTime dataNascimento) : base(id)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser nulo ou vazio.");
        if (string.IsNullOrWhiteSpace(documento))
            throw new ArgumentException("Documento não pode ser nulo ou vazio.");

        Nome = nome;
        Documento = documento;
        Contato = contato;
        DataNascimento = dataNascimento;
    }

    public Colaborador(Guid id, Guid tenantId, string nome, string documento, Contato contato, DateTime dataNascimento) : base(id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id não pode ser vazio.");
        if (tenantId == Guid.Empty)
            throw new ArgumentException("TenantId não pode ser vazio.");

        TenantId = tenantId;
        Nome = nome;
        Documento = documento;
        Contato = contato;
        DataNascimento = dataNascimento;
    }

    #region Setters
    public void SetNome(string nome)
    {
        Nome = nome;
    }
    public void SetContato(Contato contato)
    {
        Contato = contato;
    }
    #endregion
}
