using SGONGA.Core.Notifications;
using SGONGA.Core.User;
using SGONGA.WebAPI.Business.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Mappings;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.Business.Handlers;
public class SolicitacaoCadastroHandler : BaseHandler, ISolicitacaoCadastroHandler
{
    public readonly IUnitOfWork _unitOfWork;
    public readonly IONGHandler _ongHandler;
    public readonly IColaboradorHandler _colaboradorHandler;
    public readonly IIdentityHandler _identityHandler;
    public readonly SolicitacaoCadastroProvider _solicitacaoCadastroProvider;

    public SolicitacaoCadastroHandler(INotifier notifier, IAspNetUser appUser, IUnitOfWork unitOfWork, IONGHandler ongHandler, IColaboradorHandler colaboradorHandler, IIdentityHandler identityHandler, SolicitacaoCadastroProvider solicitacaoCadastroProvider) : base(notifier, appUser)
    {
        _unitOfWork = unitOfWork;
        _ongHandler = ongHandler;
        _colaboradorHandler = colaboradorHandler;
        _identityHandler = identityHandler;
        _solicitacaoCadastroProvider = solicitacaoCadastroProvider;
    }

    public async Task<SolicitacaoCadastroResponse> GetByIdAsync(GetSolicitacaoCadastroByIdRequest request)
    {
        try
        {
            if (!SolicitacaoCadastroExiste(request.Id))
            {
                Notify("Não foi possível encontrar a solicitação de cadastro.");
                return null!;
            }
            var solicitacaoCadastroDb = await _unitOfWork.SolicitacaoCadastroRepository.GetByIdAsync(request.Id);

            return solicitacaoCadastroDb.MapDomainToResponse();
        }
        catch
        {
            Notify("Não foi possível recuperar a solicitação de cadastro.");
            return null!;
        }
    }

    public async Task<PagedResponse<SolicitacaoCadastroResponse>> GetAllAsync(GetAllSolicitacoesCadastroRequest request)
    {
        try
        {
            return (await _unitOfWork.SolicitacaoCadastroRepository.GetAllPagedAsync(null, request.PageNumber, request.PageSize, request.Query, request.ReturnAll)).MapDomainToResponse();
        }
        catch
        {
            Notify("Não foi possível recuperar as solicitações de cadastro.");
            return null!;
        }
    }

    public async Task RequestRegistrationAsync(CreateSolicitacaoCadastroRequests request)
    {
        if (NomeONGEmUso(request.ONG.Nome))
        {
            Notify("Nome da ONG em uso.");
            return;
        }

        if (EmailONGEmUso(request.ONG.Contato.Email))
        {
            Notify("E-mail da ONG em uso.");
            return;
        }

        if (EmailResponsavelEmUso(request.Responsavel.Contato.Email))
        {
            Notify("E-mail do Responsável em uso.");
            return;
        }

        var requestMapped = request.MapRequestToDomain();
        try
        {
            await _unitOfWork.SolicitacaoCadastroRepository.AddAsync(requestMapped);

            await _unitOfWork.CommitAsync();
            return;
        }
        catch
        {
            Notify("Não foi possível completar a solicitação de cadastro.");
            return;
        }


    }

    public async Task UpdateStatusRequestRegistrationAsync(UpdateStatusSolicitacaoCadastroRequest request)
    {
        if (!EhSuperAdmin())
        {
            Notify("Você não tem permissão para atualizar o status da solicitação de cadastro..");
            return;
        }
        if(!SolicitacaoCadastroExiste(request.Id))
        {
            Notify("Não foi possível encontrar a solicitação de cadastro.");
            return;
        }
        var solicitacaoCadastroDb = await _unitOfWork.SolicitacaoCadastroRepository.GetByIdAsync(request.Id);
        var solicitacaoCadastromapped = solicitacaoCadastroDb.MapDomainToResponse();
        switch(request.Status)
        {
            case EStatus.Aprovado:
                await ApproveRegistration(solicitacaoCadastromapped);
                break;
            case EStatus.Cancelado:
                await DenyRegistration(solicitacaoCadastromapped);
                break;
            default:
                Notify("Não foi possível atualizar o status da solicitação de cadastro.");
                break;
        }
        return;
    }
    private async Task ApproveRegistration(SolicitacaoCadastroResponse request)
    {
        try
        {
            var ongId = request.ONG.Id;
            Guid colaboradorId = request.Responsavel.Id;
            //Cadastrar ONG
            await _ongHandler.CreateAsync(new CreateONGRequest(ongId, request.ONG.Nome, request.ONG.Instagram, request.ONG.Documento, request.ONG.ChavePix, request.ONG.Contato, request.ONG.Endereco));
            if (!IsOperationValid())
            {
                return;
            };

            //Cadastrar Colaborador
            await _colaboradorHandler.CreateAsync(new CreateColaboradorRequest(colaboradorId, ongId, request.Responsavel.Nome, request.Responsavel.Documento, request.Responsavel.DataNascimento, request.Responsavel.Contato));
            if (!IsOperationValid())
            {
                await _ongHandler.DeleteAsync(new DeleteONGRequest(ongId));
                return;
            };
            //Cadastrar Login
            string senha = "Senha@123";
            var result = await _identityHandler.CreateAsync(new CreateUserRequest(colaboradorId, request.Responsavel.Contato.Email, senha, senha));
            if (!IsOperationValid())
            {
                await _colaboradorHandler.DeleteAsync(new DeleteColaboradorRequest(colaboradorId));
                await _ongHandler.DeleteAsync(new DeleteONGRequest(ongId));
                return;
            };

            //Mudar status para Aprovado
            var solicitacaoCadastroDb = await _unitOfWork.SolicitacaoCadastroRepository.GetByIdAsync(request.Id);
            solicitacaoCadastroDb.SetStatus(EStatus.Aprovado);
            _unitOfWork.SolicitacaoCadastroRepository.UpdateAsync(solicitacaoCadastroDb);
            await _unitOfWork.CommitAsync();

            //Enviar email para o usuário informando a senha default
            // ...
            return;
        }
        catch
        {
            Notify("Não foi possível concluir a ação.");
            return;
        }
    }

    private async Task DenyRegistration(SolicitacaoCadastroResponse request)
    {
        var solicitacaoCadastroDb = await _unitOfWork.SolicitacaoCadastroRepository.GetByIdAsync(request.Id);
        solicitacaoCadastroDb.SetStatus(EStatus.Cancelado);
        _unitOfWork.SolicitacaoCadastroRepository.UpdateAsync(solicitacaoCadastroDb);
        await _unitOfWork.CommitAsync();
    }

    private bool SolicitacaoCadastroExiste(Guid id)
    {
        if (_unitOfWork.SolicitacaoCadastroRepository.SearchAsync(f => f.Id == id).Result.Any())
        {
            return true;
        };
        return false;
    }
    private bool NomeONGEmUso(string nome)
    {
        if (_unitOfWork.ONGRepository.SearchAsync(f => f.Nome == nome).Result.Any())
        {
            return true;
        };
        return false;
    }

    private bool EmailONGEmUso(string email)
    {
        if (_unitOfWork.ONGRepository.SearchAsync(f => f.Contato.Email.Endereco == email).Result.Any())
        {
            return true;
        };
        return false;
    }
    private bool EmailResponsavelEmUso(string email)
    {
        if (_unitOfWork.ColaboradorRepository.SearchWithoutTenantAsync(f => f.Contato.Email.Endereco == email).Result.Any())
        {
            return true;
        };
        return false;
    }
    private bool EhSuperAdmin()
    {
        if (AppUser.HasClaim("Permissions", "SuperAdmin"))
        {
            return true;
        }
        return false;
    }
}
