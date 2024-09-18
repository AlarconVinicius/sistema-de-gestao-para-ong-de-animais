using SGONGA.Core.Extensions;
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

public class ONGHandler(INotifier notifier, IAspNetUser appUser, IUnitOfWork unitOfWork) : BaseHandler(notifier, appUser), IONGHandler
{
    public readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<UsuarioResponse> GetByIdAsync(GetUsuarioByIdRequest request)
    {
        try
        {
            if (!ONGExiste(request.Id, request.TenantFiltro))
            {
                Notify("ONG não encontrada.");
                return null!;
            }
            var ong = request.TenantFiltro
                           ? await _unitOfWork.ONGRepository.GetByIdAsync(request.Id)
                           : await _unitOfWork.ONGRepository.GetByIdWithoutTenantAsync(request.Id);

            return ong.MapONGDomainToResponse();
        }
        catch (Exception ex)
        {
            Notify($"Não foi possível recuperar a ONG.: {ex.Message}");
            return null!;
        }
    }

    public async Task<PagedResponse<UsuarioResponse>> GetAllAsync(GetAllUsuariosRequest request)
    {
        try
        {
            var ongs = request.TenantFiltro
                           ? (await _unitOfWork.ONGRepository.GetAllPagedAsync()).MapONGDomainToResponse()
                           : (await _unitOfWork.ONGRepository.GetAllPagedWithoutTenantAsync(null, request.PageNumber, request.PageSize, request.Query, request.ReturnAll)).MapONGDomainToResponse();
            return ongs;
        }
        catch
        {
            Notify("Não foi possível recuperar as ONGs.");
            return null!;
        }
    }

    public async Task CreateAsync(CreateUsuarioRequest request)
    {
        //if (!ExecuteValidation(new ONGValidation(), ong)) return;

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
            await _unitOfWork.ONGRepository.AddAsync(request.MapRequestToONGDomain());
            return;
        }
        catch(Exception ex)
        {
            Notify($"Não foi possível criar a ONG.: {ex.Message}");
            return;
        }
    }

    public async Task UpdateAsync(UpdateUsuarioRequest request)
    {
        //if (!ExecuteValidation(new ONGValidation(), ong)) return;

        if (AppUser.GetUserId() != request.Id)
        {
            Notify("ONG não encontrada.");
            return;
        }

        if (!ONGExiste(request.Id, true))
        {
            Notify("ONG não encontrada.");
            return;
        }

        var ongDb = await _unitOfWork.ONGRepository.GetByIdAsync(request.Id);

        try
        {
            string newEmail;
            if (request.Contato.Email != ongDb.Contato.Email.Endereco)
            {
                if (EmailEmUso(request.Contato.Email))
                {
                    Notify("E-mail em uso.");
                    return;
                }
                newEmail = ongDb.Contato.Email.Endereco;
            }
            else
            {
                newEmail = request.Contato.Email;
            }
            ongDb.SetNome(request.Nome);
            ongDb.SetApelido(request.Apelido);
            ongDb.SetChavePix(request.ChavePix);
            ongDb.SetSite(request.Site);
            ongDb.SetContato(new Contato(request.Contato.Telefone, newEmail));
            ongDb.SetEndereco(request.Estado, request.Cidade);

            _unitOfWork.ONGRepository.UpdateAsync(ongDb);
            return;
        }
        catch (Exception ex)
        {
            Notify($"Não foi possível atualizar a ONG.: {ex.Message}");
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
            if (!ONGExiste(request.Id, true))
            {
                Notify("ONG não encontrada.");
                return;
            }

            var ong = await _unitOfWork.ONGRepository.GetByIdAsync(request.Id);
            if (ong.Animais.Count != 0)
            {
                foreach (Animal animal in ong.Animais)
                {
                    _unitOfWork.AnimalRepository.DeleteAsync(animal.Id);
                }
            }
            _unitOfWork.ONGRepository.DeleteAsync(request.Id);
            return;
        }
        catch (Exception ex)
        {
            Notify($"Não foi possível deletar a ONG.: {ex.Message}");
            return;
        }
    }


    private bool ONGExiste(Guid id, bool tenantFiltro)
    {
        if (tenantFiltro
            ? _unitOfWork.ONGRepository.SearchAsync(f => f.Id == id).Result.Any()
            : _unitOfWork.ONGRepository.SearchWithoutTenantAsync(f => f.Id == id).Result.Any())
        {
            return true;
        };
        return false;
    }
    private bool ApelidoEmUso(string apelido)
    {
        if (_unitOfWork.ONGRepository.SearchWithoutTenantAsync(f => f.Apelido == apelido || f.Slug == apelido.SlugifyString()).Result.Any())
        {
            return true;
        };
        return false;
    }
    private bool DocumentoEmUso(string documento)
    {
        if (_unitOfWork.ONGRepository.SearchWithoutTenantAsync(f => f.Documento == documento).Result.Any())
        {
            return true;
        };
        return false;
    }
    private bool EmailEmUso(string email)
    {
        if (_unitOfWork.ONGRepository.SearchWithoutTenantAsync(f => f.Contato.Email.Endereco == email).Result.Any())
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