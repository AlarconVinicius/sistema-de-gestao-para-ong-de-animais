using SGONGA.WebAPI.Business.Requests.Shared;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGONGA.WebAPI.Business.Responses;

public class ColaboradorResponse : Response
{
    [DisplayName("Id")]
    public Guid Id { get; set; }

    [DisplayName("Tenant Id")]
    public Guid TenantId { get; set; }

    [DisplayName("Nome")]
    public string Nome { get; set; } = string.Empty;

    [DisplayName("Documento")]
    public string Documento { get; set; } = string.Empty;

    public ContatoResponse Contato { get; set; } = null!;

    [DisplayName("Data de Nascimento")]
    public DateTime DataNascimento { get; set; }

    [DisplayName("Data de Cadastro")]
    public DateTime CreatedAt { get; set; }

    [DisplayName("Data de Modificação")]
    public DateTime UpdatedAt { get; set; }

    public ColaboradorResponse() { }

    public ColaboradorResponse(Guid id, Guid tenantId, string nome, string documento, ContatoResponse contato, DateTime dataNascimento, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        TenantId = tenantId;
        Nome = nome;
        Documento = documento;
        Contato = contato;
        DataNascimento = dataNascimento;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}