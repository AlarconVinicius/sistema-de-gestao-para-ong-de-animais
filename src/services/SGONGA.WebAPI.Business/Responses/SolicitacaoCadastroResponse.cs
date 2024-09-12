using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Requests;
using System.ComponentModel;

namespace SGONGA.WebAPI.Business.Responses;

public class SolicitacaoCadastroResponse : Response
{
    public Guid Id { get; set; }
    public CreateONGRequest ONG { get; set; } = null!;

    public CreateAdotanteRequest Responsavel { get; set; } = null!;

    public EStatus Status { get; set; }

    [DisplayName("Data de Cadastro")]
    public DateTime CreatedAt { get; set; }

    [DisplayName("Data de Modificação")]
    public DateTime UpdatedAt { get; set; }


    public SolicitacaoCadastroResponse() { }

    public SolicitacaoCadastroResponse(Guid id, CreateONGRequest ong, CreateAdotanteRequest responsavel, EStatus status, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        ONG = ong;
        Responsavel = responsavel;
        Status = status;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}