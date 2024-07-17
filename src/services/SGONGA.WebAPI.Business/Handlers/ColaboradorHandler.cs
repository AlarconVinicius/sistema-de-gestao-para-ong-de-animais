using SGONGA.Core.Notifications;
using SGONGA.Core.User;
using SGONGA.WebAPI.Business.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Mappings;
using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.Business.Handlers;

public class ColaboradorHandler : BaseHandler, IColaboradorHandler
{
    public readonly IUnitOfWork _unitOfWork;

    public ColaboradorHandler(INotifier notifier, IAspNetUser appUser, IUnitOfWork unitOfWork) : base(notifier, appUser)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ColaboradorResponse> GetByIdAsync(GetColaboradorByIdRequest request)
    {
        if (TenantIsEmpty()) return null!;
        try
        {
            if (!ColaboradorExiste(request.Id))
            {
                Notify("Colaborador não encontrado.");
                return null!;
            }
            var colaborador = await _unitOfWork.ColaboradorRepository.GetByIdAsync(request.Id, TenantId);

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
        if (TenantIsEmpty()) return null!;
        try
        {
            return (await _unitOfWork.ColaboradorRepository.GetAllPagedAsync(f => f.TenantId == TenantId, request.PageNumber, request.PageSize, request.Query, request.ReturnAll)).MapDomainToResponse();
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

        if (EmailEmUso(request.Email))
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

    public async Task UpdateAsync(UpdateColaboradorRequest request)
    {
        //if (!ExecuteValidation(new ColaboradorValidation(), colaborador)) return;

        if (TenantIsEmpty()) return;
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
            if (request.Email != colaboradorDb.Email.Endereco)
            {
                if (EmailEmUso(request.Email))
                {
                    Notify("E-mail em uso.");
                    return;
                }
                colaboradorDb.SetEmail(request.Email);
            }

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
            if (!_unitOfWork.ColaboradorRepository.SearchAsync(f => f.Id == request.Id && f.TenantId == request.TenantId).Result.Any())
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
        if (_unitOfWork.ColaboradorRepository.SearchAsync(f => f.Id == id && f.TenantId == TenantId).Result.Any())
        {
            return true;
        };
        return false;
    }

    private bool EmailEmUso(string email)
    {
        if (_unitOfWork.ColaboradorRepository.SearchAsync(f => f.Email.Endereco == email).Result.Any())
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
