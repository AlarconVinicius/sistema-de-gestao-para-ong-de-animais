using SGONGA.Core.Notifications;
using SGONGA.Core.User;
using SGONGA.WebAPI.Business.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Mappings;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Models.DomainObjects;
using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.Business.Handlers;

public class ColaboradorHandler : BaseHandler, IColaboradorHandler
{
    public readonly IUnitOfWork _unitOfWork;
    public readonly SolicitacaoCadastroProvider _solicitacaoCadastroProvider;

    public ColaboradorHandler(INotifier notifier, IAspNetUser appUser, IUnitOfWork unitOfWork, SolicitacaoCadastroProvider solicitacaoCadastroProvider) : base(notifier, appUser)
    {
        _unitOfWork = unitOfWork;
        _solicitacaoCadastroProvider = solicitacaoCadastroProvider;
    }

    public async Task<ColaboradorResponse> GetByIdAsync(GetColaboradorByIdRequest request)
    {
        try
        {
            if (!ColaboradorExiste(request.Id))
            {
                Notify("Colaborador não encontrado.");
                return null!;
            }
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.Id);

            return colaborador.MapDomainToResponse();
        }
        catch
        {
            Notify("Não foi possível recuperar o colaborador.");
            return null!;
        }
    }

    public async Task<PagedResponse<ColaboradorResponse>> GetAllAsync(GetAllColaboradoresRequest request)
    {
        try
        {
            return (await _unitOfWork.ColaboradorRepository.GetAllPagedAsync(null, request.PageNumber, request.PageSize, request.Query, request.ReturnAll)).MapDomainToResponse();
        }
        catch
        {
            Notify("Não foi possível recuperar os colaboradores.");
            return null!;
        }
    }

    public async Task CreateAsync(CreateColaboradorRequest request)
    {
        //if (!ExecuteValidation(new ColaboradorValidation(), colaborador)) return;
        if (!EhSuperAdmin())
        {
            Notify("Você não tem permissão para adicionar.");
            return;
        }

        if (EmailEmUso(request.Contato.Email))
        {
            Notify("E-mail em uso.");
            return;
        }
        var colaboradorMapped = request.MapRequestToDomain();
        try
        {
            await _unitOfWork.ColaboradorRepository.AddAsync(colaboradorMapped);

            await _unitOfWork.CommitAsync();
            return;
        }
        catch
        {
            Notify("Não foi possível criar o colaborador.");
            return;
        }
    }

    public async Task CreateAsync(CreateColaboradorRequest request, Guid tenantId)
    {
        //if (!ExecuteValidation(new ColaboradorValidation(), colaborador)) return;
        if (!EhSuperAdmin())
        {
            Notify("Você não tem permissão para adicionar.");
            return;
        }

        if (EmailEmUso(request.Contato.Email))
        {
            Notify("E-mail em uso.");
            return;
        }
        var colaboradorMapped = request.MapRequestToDomain();
        colaboradorMapped.SetTenant(tenantId);
        try
        {
            await _unitOfWork.ColaboradorRepository.AddAsync(colaboradorMapped);

            await _unitOfWork.CommitAsync();
            return;
        }
        catch
        {
            Notify("Não foi possível criar o colaborador.");
            return;
        }
    }

    public async Task UpdateAsync(UpdateColaboradorRequest request)
    {
        //if (!ExecuteValidation(new ColaboradorValidation(), colaborador)) return;

        if(AppUser.GetUserId() != request.Id)
        {
            Notify("Colaborador não encontrado.");
            return;
        }

        if (!ColaboradorExiste(request.Id))
        {
            Notify("Colaborador não encontrado.");
            return;
        }

        var colaboradorDb = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.Id);

        try
        {
            string newEmail;

            if (request.Contato.Email != colaboradorDb.Contato.Email.Endereco)
            {
                if (EmailEmUso(request.Contato.Email))
                {
                    Notify("E-mail em uso.");
                    return;
                }
                newEmail = colaboradorDb.Contato.Email.Endereco;
            }
            else
            {
                newEmail = request.Contato.Email;
            }
            colaboradorDb.SetNome(request.Nome);
            colaboradorDb.SetContato(new Contato(request.Contato.Telefone,newEmail));

            _unitOfWork.ColaboradorRepository.UpdateAsync(colaboradorDb);

            await _unitOfWork.CommitAsync();
            return;
        }
        catch
        {
            Notify("Não foi possível atualizar o Colaborador.");
            return;
        }
    }

    public async Task DeleteAsync(DeleteColaboradorRequest request)
    {
        if (!EhSuperAdmin())
        {
            Notify("Você não tem permissão para deletar.");
            return;
        }
        try
        {
            if (!_unitOfWork.ColaboradorRepository.SearchAsync(f => f.Id == request.Id).Result.Any())
            {
                Notify("Colaborador não encontrado.");
                return;
            }

            _unitOfWork.ColaboradorRepository.DeleteAsync(request.Id);

            await _unitOfWork.CommitAsync();
            return;
        }
        catch
        {
            Notify("Não foi possível deletar o colaborador.");
            return;
        }
    }

    private bool ColaboradorExiste(Guid id)
    {
        if (_unitOfWork.ColaboradorRepository.SearchAsync(f => f.Id == id).Result.Any())
        {
            return true;
        };
        return false;
    }

    private bool EmailEmUso(string email)
    {
        if (_unitOfWork.ColaboradorRepository.SearchAsync(f => f.Contato.Email.Endereco == email).Result.Any())
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
