﻿using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.Business.Interfaces.Handlers;

public interface IAnimalHandler
{
    Task CreateAsync(CreateAnimalRequest request);
    Task UpdateAsync(UpdateAnimalRequest request);
    Task DeleteAsync(DeleteAnimalRequest request);
    Task<AnimalResponse> GetByIdAsync(GetAnimalByIdRequest request);
    Task<PagedResponse<AnimalResponse>> GetAllAsync(GetAllAnimaisRequest request);
}