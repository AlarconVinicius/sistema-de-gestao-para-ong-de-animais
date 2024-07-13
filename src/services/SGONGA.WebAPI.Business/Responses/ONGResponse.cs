using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGONGA.WebAPI.Business.Responses;

public class ONGResponse : Response
{
    [DisplayName("Id")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Nome")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Descrição")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Telefone")]
    public string Telefone { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Chave Pix")]
    public string ChavePix { get; set; } = string.Empty;

    [DisplayName("Data de Cadastro")]
    public DateTime CreatedAt { get; set; }

    [DisplayName("Data de Modificação")]
    public DateTime UpdatedAt { get; set; }

    public EnderecoResponse? Endereco { get; set; }
    public ONGResponse() { }

    public ONGResponse(Guid id, string nome, string descricao, string telefone, string email, string chavePix, DateTime createdAt, DateTime updatedAt, EnderecoResponse? endereco = null)
    {
        Id = id;
        Nome = nome;
        Descricao = descricao;
        Telefone = telefone;
        Email = email;
        ChavePix = chavePix;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Endereco = endereco;
    }
}
