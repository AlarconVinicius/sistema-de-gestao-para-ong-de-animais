using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGONGA.WebAPI.Business.Responses;

public class ColaboradorResponse : Response
{
    [DisplayName("Id")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("E-mail")]
    public string Email { get; set; } = string.Empty;

    [DisplayName("Data de Cadastro")]
    public DateTime CreatedAt { get; set; }

    [DisplayName("Data de Modificação")]
    public DateTime UpdatedAt { get; set; }

    public ColaboradorResponse() { }

    public ColaboradorResponse(Guid id, string email, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        Email = email;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}