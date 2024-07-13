using SGONGA.Core.Notifications;
using SGONGA.Core.User;
using SGONGA.WebAPI.Business.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Mappings;
using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.Business.Handlers;

public class AnimalHandler : BaseHandler, IAnimalHandler
{
    public readonly IUnitOfWork _unitOfWork;

    public AnimalHandler(INotifier notifier, IAspNetUser appUser, IUnitOfWork unitOfWork) : base(notifier, appUser)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AnimalResponse> GetByIdAsync(GetAnimalByIdRequest request)
    {
        if (TenantIsEmpty()) return null!;
        try
        {
            if (!AnimalExiste(request.Id))
            {
                Notify("Animal não encontrado.");
                return null!;
            }
            var animal = await _unitOfWork.AnimalRepository.GetByIdAsync(request.Id, TenantId);

            return animal.MapDomainToResponse();
        }
        catch
        {
            Notify("Não foi possível recuperar o animal.");
            return null!;
        }
    }

    public async Task<PagedResponse<AnimalResponse>> GetAllAsync(GetAllAnimaisRequest request)
    {
        if (TenantIsEmpty()) return null!;
        try
        {
            return (await _unitOfWork.AnimalRepository.GetAllPagedAsync(f => f.TenantId == TenantId, request.PageNumber, request.PageSize, request.Query, request.ReturnAll)).MapDomainToResponse();
        }
        catch
        {
            Notify("Não foi possível recuperar as animais.");
            return null!;
        }
    }

    public async Task CreateAsync(CreateAnimalRequest request)
    {
        //if (!ExecuteValidation(new AnimalValidation(), animal)) return;

        if (TenantIsEmpty()) return;

        request.TenantId = TenantId;
        var animalMapped = request.MapRequestToDomain();
        try
        {
            await _unitOfWork.AnimalRepository.AddAsync(animalMapped);

            await _unitOfWork.CommitAsync();
            return;
        }
        catch
        {
            Notify("Não foi possível criar o animal.");
            return;
        }
    }

    public async Task UpdateAsync(UpdateAnimalRequest request)
    {
        //if (!ExecuteValidation(new AnimalValidation(), animal)) return;

        if (TenantIsEmpty()) return;

        if (!AnimalExiste(request.Id))
        {
            Notify("Animal não encontrado.");
            return;
        }

        var animalDb = await _unitOfWork.AnimalRepository.GetByIdAsync(request.Id);

        try
        {
            animalDb.SetNome(request.Nome);
            animalDb.SetCor(request.Cor);
            animalDb.SetEspecie(request.Especie);
            animalDb.SetRaca(request.Raca);
            animalDb.SetDescricao(request.Descricao);
            animalDb.SetObservacao(request.Observacao);
            animalDb.SetChavePix(request.ChavePix);
            animalDb.SetPorte(request.Porte);
            animalDb.SetFotos(request.Fotos);
            await _unitOfWork.AnimalRepository.UpdateAsync(animalDb);

            await _unitOfWork.CommitAsync();
            return;
        }
        catch
        {
            Notify("Não foi possível atualizar o Animal.");
            return;
        }
    }

    public async Task DeleteAsync(DeleteAnimalRequest request)
    {
        if (TenantIsEmpty()) return;

        try
        {
            if (!AnimalExiste(request.Id))
            {
                Notify("Animal não encontrado.");
                return;
            }

            await _unitOfWork.AnimalRepository.DeleteAsync(request.Id);

            await _unitOfWork.CommitAsync();
            return;
        }
        catch
        {
            Notify("Não foi possível deletar o Animal.");
            return;
        }
    }

    private bool AnimalExiste(Guid id)
    {
        if (_unitOfWork.AnimalRepository.SearchAsync(f => f.Id == id && f.TenantId == TenantId).Result.Any())
        {
            return true;
        };
        return false;
    }
}