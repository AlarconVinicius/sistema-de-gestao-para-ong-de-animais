using SGONGA.Core.Notifications;
using SGONGA.Core.User;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Errors;
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

    public async Task<Result<AnimalResponse>> GetByIdAsync(GetAnimalByIdRequest request)
    {
        if (AnimalExiste(request.Id, request.TenantFiltro).IsFailed)
            return AnimalErrors.AnimalNaoEncontrado(request.Id);

        var result = request.TenantFiltro
                           ? await _unitOfWork.AnimalRepository.GetByIdAsync(request.Id)
                           : await _unitOfWork.AnimalRepository.GetByIdWithoutTenantAsync(request.Id);

        return result.Value.MapDomainToResponse();
    }

    public async Task<Result<PagedResponse<AnimalResponse>>> GetAllAsync(GetAllAnimaisRequest request)
    {
        var results = request.TenantFiltro
                           ? await _unitOfWork.AnimalRepository.GetAllPagedAsync(null, request.PageNumber, request.PageSize, request.Query, request.ReturnAll)
                           : await _unitOfWork.AnimalRepository.GetAllPagedWithoutTenantAsync(null, request.PageNumber, request.PageSize, request.Query, request.ReturnAll);

        return results.Value.MapDomainToResponse();
    }

    public async Task<Result> CreateAsync(CreateAnimalRequest request)
    {
        //if (!ExecuteValidation(new AnimalValidation(), animal)) return;

        var animalMapped = request.MapRequestToDomain();

        await _unitOfWork.AnimalRepository.AddAsync(animalMapped);

        var result = await _unitOfWork.CommitAsync();

        return result.IsSuccess ? Result.Ok() : result.Errors;
    }

    public async Task<Result> UpdateAsync(UpdateAnimalRequest request)
    {
        //if (!ExecuteValidation(new AnimalValidation(), animal)) return;

        if (AnimalExiste(request.Id, true).IsFailed)
            return AnimalErrors.AnimalNaoEncontrado(request.Id);

        var animalDb = (await _unitOfWork.AnimalRepository.GetByIdAsync(request.Id)).Value;

        animalDb.SetNome(request.Nome);
        animalDb.SetCor(request.Cor);
        animalDb.SetEspecie(request.Especie);
        animalDb.SetRaca(request.Raca);
        animalDb.SetDescricao(request.Descricao);
        animalDb.SetObservacao(request.Observacao);
        animalDb.SetChavePix(request.ChavePix);
        animalDb.SetPorte(request.Porte);
        animalDb.SetFoto(request.Foto);

        _unitOfWork.AnimalRepository.Update(animalDb);

        var result = await _unitOfWork.CommitAsync();

        return result.IsSuccess ? Result.Ok() : result.Errors;
    }

    public async Task<Result> DeleteAsync(DeleteAnimalRequest request)
    {
        if (AnimalExiste(request.Id, true).IsFailed)
            return AnimalErrors.AnimalNaoEncontrado(request.Id);

        _unitOfWork.AnimalRepository.Delete(request.Id);

        var result = await _unitOfWork.CommitAsync();

        return result.IsSuccess ? Result.Ok() : result.Errors;
    }

    private Result AnimalExiste(Guid id, bool tenantFiltro)
    {
        var exists = tenantFiltro
            ? _unitOfWork.AnimalRepository.SearchAsync(f => f.Id == id).Result.Value.Any()
            : _unitOfWork.AnimalRepository.SearchWithoutTenantAsync(f => f.Id == id).Result.Value.Any();

        return exists ? Result.Ok() : Error.NullValue;
    }
}