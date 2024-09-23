using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.Business.Interfaces.Handlers;

public interface IAnimalHandler
{
    Task<Result> CreateAsync(CreateAnimalRequest request);
    Task<Result> UpdateAsync(UpdateAnimalRequest request);
    Task<Result> DeleteAsync(DeleteAnimalRequest request);
    Task<Result<AnimalResponse>> GetByIdAsync(GetAnimalByIdRequest request);
    Task<Result<PagedResponse<AnimalResponse>>> GetAllAsync(GetAllAnimaisRequest request);
}