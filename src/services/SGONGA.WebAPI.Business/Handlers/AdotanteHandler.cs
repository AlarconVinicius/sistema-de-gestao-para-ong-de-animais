using SGONGA.Core.Extensions;
using SGONGA.Core.Notifications;
using SGONGA.Core.User;
using SGONGA.WebAPI.Business.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Mappings;
using SGONGA.WebAPI.Business.Models.DomainObjects;
using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.Business.Handlers;

public class AdotanteHandler(INotifier notifier, IAspNetUser appUser, IUnitOfWork unitOfWork) : BaseHandler(notifier, appUser), IAdotanteHandler
{
    public readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<UsuarioResponse> GetByIdAsync(GetUsuarioByIdRequest request)
    {
        try
        {
            if (!AdotanteExiste(request.Id, request.TenantFiltro))
            {
                Notify("Adotante não encontrado.");
                return null!;
            }
            var adotante = request.TenantFiltro
                           ? await _unitOfWork.AdotanteRepository.GetByIdAsync(request.Id) 
                           : await _unitOfWork.AdotanteRepository.GetByIdWithoutTenantAsync(request.Id);

            return adotante.MapAdotanteDomainToResponse();
        }
        catch (Exception ex)
        {
            Notify($"Não foi possível recuperar o adotante.: {ex.Message}");
            return null!;
        }
    }

    public async Task<PagedResponse<UsuarioResponse>> GetAllAsync(GetAllUsuariosRequest request)
    {
        try
        {
            var adotantes = request.TenantFiltro
                           ? (await _unitOfWork.AdotanteRepository.GetAllPagedAsync()).MapAdotanteDomainToResponse()
                           : (await _unitOfWork.AdotanteRepository.GetAllPagedWithoutTenantAsync(null, request.PageNumber, request.PageSize, request.Query, request.ReturnAll)).MapAdotanteDomainToResponse();
            return adotantes;
        }
        catch (Exception ex)
        {
            Notify($"Não foi possível recuperar os adotantes.: {ex.Message}");
            return null!;
        }
    }

    public async Task CreateAsync(CreateUsuarioRequest request)
    {
        //if (!ExecuteValidation(new AdotanteValidation(), adotante)) return;
        if (DocumentoEmUso(request.Documento))
        {
            Notify("E-mail em uso.");
            return;
        }
        if (ApelidoEmUso(request.Apelido))
        {
            Notify("Nome em uso.");
            return;
        }

        if (EmailEmUso(request.Contato.Email))
        {
            Notify("E-mail em uso.");
            return;
        }
        try
        {
            await _unitOfWork.AdotanteRepository.AddAsync(request.MapRequestToAdotanteDomain());
            return;
        }
        catch (Exception ex)
        {
            Notify($"Não foi possível criar o adotante.: {ex.Message}");
            return;
        }
    }

    public async Task UpdateAsync(UpdateUsuarioRequest request)
    {
        //if (!ExecuteValidation(new AdotanteValidation(), adotante)) return;

        if(AppUser.GetUserId() != request.Id)
        {
            Notify("Adotante não encontrado.");
            return;
        }

        if (!AdotanteExiste(request.Id, true))
        {
            Notify("Adotante não encontrado.");
            return;
        }

        var adotanteDb = await _unitOfWork.AdotanteRepository.GetByIdAsync(request.Id);

        try
        {
            string newEmail;

            if (request.Contato.Email != adotanteDb.Contato.Email.Endereco)
            {
                if (EmailEmUso(request.Contato.Email))
                {
                    Notify("E-mail em uso.");
                    return;
                }
                newEmail = adotanteDb.Contato.Email.Endereco;
            }
            else
            {
                newEmail = request.Contato.Email;
            }
            adotanteDb.SetNome(request.Nome);
            adotanteDb.SetApelido(request.Apelido);
            adotanteDb.SetSite(request.Site);
            adotanteDb.SetContato(new Contato(request.Contato.Telefone, newEmail));
            adotanteDb.SetEndereco(request.Estado, request.Cidade);

            _unitOfWork.AdotanteRepository.UpdateAsync(adotanteDb);
            return;
        }
        catch (Exception ex)
        {
            Notify($"Não foi possível atualizar o Adotante.: {ex.Message}");
            return;
        }
    }

    public async Task DeleteAsync(DeleteUsuarioRequest request)
    {
        if (AppUser.GetUserId() != request.Id && !EhSuperAdmin())
        {
            Notify("Você não tem permissão para deletar.");
            return;
        }
        try
        {
            if (!AdotanteExiste(request.Id, true))
            {
                Notify("Adotante não encontrado.");
                return;
            }

            _unitOfWork.AdotanteRepository.DeleteAsync(request.Id);
            return;
        }
        catch (Exception ex)
        {
            Notify($"Não foi possível deletar o adotante.: {ex.Message}");
            return;
        }
    }


    private bool AdotanteExiste(Guid id, bool tenantFiltro)
    {
        if (tenantFiltro 
            ? _unitOfWork.AdotanteRepository.SearchAsync(f => f.Id == id).Result.Any() 
            : _unitOfWork.AdotanteRepository.SearchWithoutTenantAsync(f => f.Id == id).Result.Any())
        {
            return true;
        };
        return false;
    }
    private bool ApelidoEmUso(string apelido)
    {
        if (_unitOfWork.AdotanteRepository.SearchWithoutTenantAsync(f => f.Apelido == apelido || f.Slug == apelido.SlugifyString()).Result.Any())
        {
            return true;
        };
        return false;
    }
    private bool DocumentoEmUso(string documento)
    {
        if (_unitOfWork.AdotanteRepository.SearchWithoutTenantAsync(f => f.Documento == documento).Result.Any())
        {
            return true;
        };
        return false;
    }
    private bool EmailEmUso(string email)
    {
        if (_unitOfWork.AdotanteRepository.SearchWithoutTenantAsync(f => f.Contato.Email.Endereco == email).Result.Any())
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
