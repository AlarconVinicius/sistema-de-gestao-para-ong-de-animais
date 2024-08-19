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

public class ONGHandler : BaseHandler, IONGHandler
{
    public readonly IUnitOfWork _unitOfWork;
    public readonly SolicitacaoCadastroProvider _solicitacaoCadastroProvider;

    public ONGHandler(INotifier notifier, IAspNetUser appUser, IUnitOfWork unitOfWork, SolicitacaoCadastroProvider solicitacaoCadastroProvider) : base(notifier, appUser)
    {
        _unitOfWork = unitOfWork;
        _solicitacaoCadastroProvider = solicitacaoCadastroProvider;
    }

    public async Task<ONGResponse> GetByIdAsync(GetONGByIdRequest request)
    {
        try
        {
            ONG ong;

            if (!ONGExiste(request.Id))
            {
                Notify("ONG não encontrada.");
                return null!;
            }
            if (request.TenantFiltro)
            {
                ong = await _unitOfWork.ONGRepository.GetByIdAsync(request.Id);
                if (ong is null) return null!;
            }
            else
            {
                ong = await _unitOfWork.ONGRepository.GetByIdWithoutTenantAsync(request.Id);
                if (ong is null) return null!;
            }

            return ong.MapDomainToResponse();
        }
        catch
        {
            Notify("Não foi possível recuperar a ONG.");
            return null!;
        }
    }

    public async Task<PagedResponse<ONGResponse>> GetAllAsync(GetAllONGsRequest request)
    {
        try
        {
            if (request.TenantFiltro)
            {
                return (await _unitOfWork.ONGRepository.GetAllPagedAsync(null, request.PageNumber, request.PageSize, request.Query, request.ReturnAll)).MapDomainToResponse();
            }
            return (await _unitOfWork.ONGRepository.GetAllPagedWithoutTenantAsync(null, request.PageNumber, request.PageSize, request.Query, request.ReturnAll)).MapDomainToResponse();
        }
        catch
        {
            Notify("Não foi possível recuperar as ONGs.");
            return null!;
        }
    }

    public async Task CreateAsync(CreateONGRequest request)
    {
        //if (!ExecuteValidation(new ONGValidation(), ong)) return;

        if (!EhSuperAdmin())
        {
            Notify("Você não tem permissão para adicionar.");
            return;
        }
        if (NomeEmUso(request.Nome))
        {
            Notify("Nome em uso.");
            return;
        }

        if (EmailEmUso(request.Contato.Email))
        {
            Notify("E-mail em uso.");
            return;
        }
        var ongMapped = request.MapRequestToDomain();
        try
        {
            await _unitOfWork.ONGRepository.AddAsync(ongMapped);

            await _unitOfWork.CommitAsync();
            _solicitacaoCadastroProvider.OngId = ongMapped.Id;
            return;
        }
        catch
        {
            Notify("Não foi possível criar a ONG.");
            return;
        }
    }

    public async Task UpdateAsync(UpdateONGRequest request)
    {
        //if (!ExecuteValidation(new ONGValidation(), ong)) return;
        if (TenantIsEmpty()) return;
        if (request.Id != TenantId)
        {
            Notify("ONG não encontrada.");
            return;
        }
        if (!ONGExiste(request.Id))
        {
            Notify("ONG não encontrada.");
            return;
        }

        var ongDb = await _unitOfWork.ONGRepository.GetByIdAsync(request.Id);

        try
        {
            if (request.Contato.Email != ongDb.Contato.Email.Endereco)
            {
                if (EmailEmUso(request.Contato.Email))
                {
                    Notify("E-mail em uso.");
                    return;
                }
                ongDb.SetContato(new Contato(request.Contato.Telefone, request.Contato.Email));
            }
            ongDb.SetNome(request.Nome);
            ongDb.SetInstagram(request.Instagram);
            ongDb.SetChavePix(request.ChavePix);
            ongDb.SetEndereco(new Endereco(request.Endereco.Cidade, request.Endereco.Estado, request.Endereco.CEP, request.Endereco.Logradouro, request.Endereco.Bairro, request.Endereco.Numero, request.Endereco.Complemento, request.Endereco.Referencia));

            _unitOfWork.ONGRepository.UpdateAsync(ongDb);

            await _unitOfWork.CommitAsync();
            return;
        }
        catch
        {
            Notify("Não foi possível atualizar a ONG.");
            return;
        }
    }

    public async Task DeleteAsync(DeleteONGRequest request)
    {
        try
        {
            if (!EhSuperAdmin())
            {
                Notify("Você não tem permissão para deletar.");
                return;
            }
            if (!ONGExiste(request.Id))
            {
                Notify("ONG não encontrada.");
                return;
            }

            _unitOfWork.ONGRepository.DeleteAsync(request.Id);

            await _unitOfWork.CommitAsync();
            return;
        }
        catch
        {
            Notify("Não foi possível deletar a ONG.");
            return;
        }
    }

    private bool ONGExiste(Guid id)
    {
        if (_unitOfWork.ONGRepository.SearchAsync(f => f.Id == id).Result.Any())
        {
            return true;
        };
        return false;
    }

    private bool NomeEmUso(string nome)
    {
        if (_unitOfWork.ONGRepository.SearchAsync(f => f.Nome == nome).Result.Any())
        {
            return true;
        };
        return false;
    }

    private bool EmailEmUso(string email)
    {
        if (_unitOfWork.ONGRepository.SearchAsync(f => f.Contato.Email.Endereco == email).Result.Any())
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