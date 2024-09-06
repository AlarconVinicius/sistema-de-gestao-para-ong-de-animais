using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.Business.Interfaces.Handlers;

public interface ISolicitacaoCadastroHandler
{
    Task RequestRegistrationAsync(CreateSolicitacaoCadastroRequests request);
    Task UpdateStatusRequestRegistrationAsync(UpdateStatusSolicitacaoCadastroRequest request);
    Task<SolicitacaoCadastroResponse> GetByIdAsync(GetSolicitacaoCadastroByIdRequest request);
    Task<PagedResponse<SolicitacaoCadastroResponse>> GetAllAsync(GetAllSolicitacoesCadastroRequest request);
}