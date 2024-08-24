using SGONGA.WebAPI.Business.Requests.Shared;
using System.ComponentModel;

namespace SGONGA.WebAPI.Business.Responses;

public class ONGResponse : Response
{
    [DisplayName("Id")]
    public Guid Id { get; set; }

    [DisplayName("Nome")]
    public string Nome { get; set; } = string.Empty;

    [DisplayName("Instagram")]
    public string Instagram { get; set; } = string.Empty;
    
    [DisplayName("Documento")]
    public string Documento { get; set; } = string.Empty;
    public EnderecoResponse? Endereco { get; set; }

    public ContatoResponse Contato { get; set; } = null!;

    [DisplayName("Chave Pix")]
    public string? ChavePix { get; set; } = string.Empty;

    [DisplayName("Data de Cadastro")]
    public DateTime CreatedAt { get; set; }

    [DisplayName("Data de Modificação")]
    public DateTime UpdatedAt { get; set; }

    public ONGResponse() { }

    public ONGResponse(Guid id, string nome, string instagram, string documento, ContatoResponse contato, EnderecoResponse? endereco, string? chavePix, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        Nome = nome;
        Instagram = instagram;
        Documento = documento;
        ChavePix = chavePix;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Endereco = endereco;
        Contato = contato;
    }
}
