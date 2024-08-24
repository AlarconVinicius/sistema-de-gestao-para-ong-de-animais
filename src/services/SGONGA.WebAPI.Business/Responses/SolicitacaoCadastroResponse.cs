using SGONGA.WebAPI.Business.Requests;
using System.ComponentModel;

namespace SGONGA.WebAPI.Business.Responses;

public class SolicitacaoCadastroResponse : Response
{
    public Guid Id { get; set; }
    public CreateONGRequest ONG { get; set; } = null!;

    public CreateColaboradorRequest Responsavel { get; set; } = null!;

    [DisplayName("Data de Cadastro")]
    public DateTime CreatedAt { get; set; }

    [DisplayName("Data de Modificação")]
    public DateTime UpdatedAt { get; set; }


    public SolicitacaoCadastroResponse() { }

    public SolicitacaoCadastroResponse(Guid id, CreateONGRequest ong, CreateColaboradorRequest responsavel, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        ONG = ong;
        Responsavel = responsavel;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}