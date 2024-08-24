using SGONGA.WebAPI.Business.Models;

namespace SGONGA.WebAPI.Business.Requests;

public class CreateSolicitacaoCadastroRequests : Request
{
    public CreateONGRequest ONG { get; set; } = null!;

    public CreateColaboradorRequest Responsavel { get; set; } = null!;

    public EStatus Status { get; set; }
    public CreateSolicitacaoCadastroRequests() { }

    public CreateSolicitacaoCadastroRequests(CreateONGRequest ong, CreateColaboradorRequest responsavel)
    {
        ONG = ong;
        Responsavel = responsavel;
        Status = EStatus.Pendente;
    }
}

public class UpdateStatusSolicitacaoCadastroRequest : Request
{
    public Guid Id { get; set; }

    public EStatus Status { get; set; }


    public UpdateStatusSolicitacaoCadastroRequest() { }

    public UpdateStatusSolicitacaoCadastroRequest(Guid id, EStatus status)
    {
        Id = id;
        Status = status;
    }
}

public class GetAllSolicitacoesCadastroRequest : PagedRequest
{
    public GetAllSolicitacoesCadastroRequest() { }

    public GetAllSolicitacoesCadastroRequest(int pageSize = 50, int pageNumber = 1, string? query = null, bool returnAll = false) : base(pageSize, pageNumber, query, returnAll)
    {

    }
}

public class GetSolicitacaoCadastroByIdRequest : Request
{
    public Guid Id { get; set; }

    public GetSolicitacaoCadastroByIdRequest() { }

    public GetSolicitacaoCadastroByIdRequest(Guid id)
    {
        Id = id;
    }
}