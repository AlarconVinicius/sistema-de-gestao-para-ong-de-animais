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

public class AdotanteHandler : BaseHandler, IAdotanteHandler
{
    public readonly IUnitOfWork _unitOfWork;
    public readonly SolicitacaoCadastroProvider _solicitacaoCadastroProvider;

    public AdotanteHandler(INotifier notifier, IAspNetUser appUser, IUnitOfWork unitOfWork, SolicitacaoCadastroProvider solicitacaoCadastroProvider) : base(notifier, appUser)
    {
        _unitOfWork = unitOfWork;
        _solicitacaoCadastroProvider = solicitacaoCadastroProvider;
    }

    public async Task<AdotanteResponse> GetByIdAsync(GetAdotanteByIdRequest request)
    {
        try
        {
            if (!AdotanteExiste(request.Id))
            {
                Notify("Adotante não encontrado.");
                return null!;
            }
            var adotante = await _unitOfWork.AdotanteRepository.GetByIdAsync(request.Id);

            return adotante.MapDomainToResponse();
        }
        catch
        {
            Notify("Não foi possível recuperar o adotante.");
            return null!;
        }
    }

    public async Task<PagedResponse<AdotanteResponse>> GetAllAsync(GetAllAdotantesRequest request)
    {
        try
        {
            return (await _unitOfWork.AdotanteRepository.GetAllPagedAsync(null, request.PageNumber, request.PageSize, request.Query, request.ReturnAll)).MapDomainToResponse();
        }
        catch
        {
            Notify("Não foi possível recuperar os adotantes.");
            return null!;
        }
    }

    public async Task CreateAsync(CreateAdotanteRequest request)
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
            await _unitOfWork.AdotanteRepository.AddAsync(request.MapRequestToDomain());
            return;
        }
        catch
        {
            Notify("Não foi possível criar o adotante.");
            return;
        }
    }

    public async Task UpdateAsync(UpdateAdotanteRequest request)
    {
        //if (!ExecuteValidation(new AdotanteValidation(), adotante)) return;

        if(AppUser.GetUserId() != request.Id)
        {
            Notify("Adotante não encontrado.");
            return;
        }

        if (!AdotanteExiste(request.Id))
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
            adotanteDb.SetContato(new Contato(request.Contato.Telefone,newEmail));

            _unitOfWork.AdotanteRepository.UpdateAsync(adotanteDb);

            await _unitOfWork.CommitAsync();
            return;
        }
        catch
        {
            Notify("Não foi possível atualizar o Adotante.");
            return;
        }
    }

    public async Task DeleteAsync(DeleteAdotanteRequest request)
    {
        if (!EhSuperAdmin())
        {
            Notify("Você não tem permissão para deletar.");
            return;
        }
        try
        {
            if (!_unitOfWork.AdotanteRepository.SearchAsync(f => f.Id == request.Id).Result.Any())
            {
                Notify("Adotante não encontrado.");
                return;
            }

            _unitOfWork.AdotanteRepository.DeleteAsync(request.Id);

            await _unitOfWork.CommitAsync();
            return;
        }
        catch
        {
            Notify("Não foi possível deletar o adotante.");
            return;
        }
    }

    private bool AdotanteExiste(Guid id)
    {
        if (_unitOfWork.AdotanteRepository.SearchAsync(f => f.Id == id).Result.Any())
        {
            return true;
        };
        return false;
    }
    private bool ApelidoEmUso(string apelido)
    {
        if (_unitOfWork.AdotanteRepository.SearchAsync(f => f.Apelido == apelido || f.Slug == apelido.SlugifyString()).Result.Any())
        {
            return true;
        };
        return false;
    }

    private bool DocumentoEmUso(string documento)
    {
        if (_unitOfWork.AdotanteRepository.SearchAsync(f => f.Documento == documento).Result.Any())
        {
            return true;
        };
        return false;
    }
    private bool EmailEmUso(string email)
    {
        if (_unitOfWork.AdotanteRepository.SearchAsync(f => f.Contato.Email.Endereco == email).Result.Any())
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
