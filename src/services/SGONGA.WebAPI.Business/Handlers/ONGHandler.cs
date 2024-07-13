using SGONGA.Core.Notifications;
using SGONGA.Core.User;
using SGONGA.WebAPI.Business.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Mappings;
using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.Business.Handlers;

public class ONGHandler : BaseHandler, IONGHandler
{
    public readonly IUnitOfWork _unitOfWork;

    public ONGHandler(INotifier notifier, IAspNetUser appUser, IUnitOfWork unitOfWork) : base(notifier, appUser)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ONGResponse> GetByIdAsync(GetONGByIdRequest request)
    {
        try
        {
            if (!ONGExiste(request.Id)) return null!;
            var ong = await _unitOfWork.ONGRepository.GetByIdAsync(request.Id);

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
            return (await _unitOfWork.ONGRepository.GetAllPagedAsync(null, request.PageNumber, request.PageSize, request.Query, request.ReturnAll)).MapDomainToResponse();
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

        if (NomeEmUso(request.Nome))
        {
            Notify("Nome em uso.");
            return; 
        }

        if (EmailEmUso(request.Email))
        {
            Notify("E-mail em uso.");
            return;
        }
        var ongMapped = request.MapRequestToDomain();
        try
        {
            await _unitOfWork.ONGRepository.AddAsync(ongMapped);

            await _unitOfWork.CommitAsync();
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

        if (!ONGExiste(request.Id))
        {
            Notify("ONG não encontrada.");
            return;
        }

        var ongDb = await _unitOfWork.ONGRepository.GetByIdAsync(request.Id);

        try
        {
            ongDb.SetNome(request.Nome);
            ongDb.SetDescricao(request.Descricao);
            ongDb.SetChavePix(request.ChavePix);
            ongDb.SetContato(request.Telefone, request.Email);
            ongDb.SetEndereco(request.Rua, request.Cidade, request.Estado, request.CEP, request.Complemento);

            await _unitOfWork.ONGRepository.UpdateAsync(ongDb);

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
            if (!ONGExiste(request.Id))
            {
                Notify("ONG não encontrada.");
                return;
            }

            await _unitOfWork.ONGRepository.DeleteAsync(request.Id);

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
}