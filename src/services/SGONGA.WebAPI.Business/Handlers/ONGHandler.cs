using SGONGA.Core.Extensions;
using SGONGA.Core.Notifications;
using SGONGA.Core.User;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Errors;
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

    public async Task<Result<UsuarioResponse>> GetByIdAsync(GetUsuarioByIdRequest request)
    {
        if (ONGExiste(request.Id, request.TenantFiltro).IsFailed)
            return ONGErrors.ONGNaoEncontrada(request.Id);

        var result = request.TenantFiltro
                       ? await _unitOfWork.ONGRepository.GetByIdAsync(request.Id)
                       : await _unitOfWork.ONGRepository.GetByIdWithoutTenantAsync(request.Id);

        return result.Value.MapONGDomainToResponse();
    }

    public async Task<Result<PagedResponse<UsuarioResponse>>> GetAllAsync(GetAllUsuariosRequest request)
    {
        var results = request.TenantFiltro
                           ? await _unitOfWork.ONGRepository.GetAllPagedAsync(null, request.PageNumber, request.PageSize, request.Query, request.ReturnAll)
                           : await _unitOfWork.ONGRepository.GetAllPagedWithoutTenantAsync(null, request.PageNumber, request.PageSize, request.Query, request.ReturnAll);

        return results.Value.MapONGDomainToResponse();
    }

    public async Task<Result> CreateAsync(CreateUsuarioRequest request)
    {
        //if (!ExecuteValidation(new ONGValidation(), ong)) return;

        if (DocumentoDisponivel(request.Documento).IsFailed)
            return ValidationErrors.DocumentoEmUso(request.Documento);

        if (ApelidoDisponivel(request.Apelido).IsFailed)
            return ValidationErrors.ApelidoEmUso(request.Apelido);

        if (EmailDisponivel(request.Contato.Email).IsFailed)
            return ValidationErrors.EmailEmUso(request.Contato.Email);

        await _unitOfWork.ONGRepository.AddAsync(request.MapRequestToONGDomain());

        return Result.Ok();
    }

    public async Task<Result> UpdateAsync(UpdateUsuarioRequest request)
    {
        //if (!ExecuteValidation(new ONGValidation(), ong)) return;

        if (AppUser.GetUserId() != request.Id
            || ONGExiste(request.Id, true).IsFailed)
            return ONGErrors.ONGNaoEncontrada(request.Id);

        var resultDb = (await _unitOfWork.ONGRepository.GetByIdAsync(request.Id)).Value;

        string newEmail;

        if (request.Contato.Email != resultDb.Contato.Email.Endereco)
        {
            if (EmailDisponivel(request.Contato.Email).IsFailed)
                return ValidationErrors.EmailEmUso(request.Contato.Email);

            newEmail = resultDb.Contato.Email.Endereco;
        }
        else
        {
            newEmail = request.Contato.Email;
        }
        resultDb.SetNome(request.Nome);
        resultDb.SetApelido(request.Apelido);
        resultDb.SetChavePix(request.ChavePix);
        resultDb.SetSite(request.Site);
        resultDb.SetContato(new Contato(request.Contato.Telefone, newEmail));
        resultDb.SetEndereco(request.Estado, request.Cidade);

        _unitOfWork.ONGRepository.Update(resultDb);

        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(DeleteUsuarioRequest request)
    {
        if (AppUser.GetUserId() != request.Id && EhSuperAdmin().IsFailed)
            return Error.AccessDenied;

        if (ONGExiste(request.Id, true).IsFailed)
            return ONGErrors.ONGNaoEncontrada(request.Id);

        var resultsDb = (await _unitOfWork.ONGRepository.GetByIdAsync(request.Id)).Value;

        if (resultsDb.Animais.Count != 0)
        {
            foreach (Animal result in resultsDb.Animais)
            {
                _unitOfWork.AnimalRepository.Delete(result.Id);
            }
        }

        _unitOfWork.ONGRepository.Delete(request.Id);

        return Result.Ok();
    }


    private Result ONGExiste(Guid id, bool tenantFiltro)
    {
        var exists = tenantFiltro
        ? _unitOfWork.ONGRepository.SearchAsync(f => f.Id == id).Result.Value.Any()
        : _unitOfWork.ONGRepository.SearchWithoutTenantAsync(f => f.Id == id).Result.Value.Any();

        return exists ? Result.Ok() : Error.NullValue;
    }
    private Result ApelidoDisponivel(string apelido)
    {
        var available = !_unitOfWork.ONGRepository.SearchWithoutTenantAsync(f => f.Apelido == apelido || f.Slug == apelido.SlugifyString()).Result.Value.Any();

        return available ? Result.Ok() : Error.NullValue;
    }
    private Result DocumentoDisponivel(string documento)
    {
        var available = !_unitOfWork.ONGRepository.SearchWithoutTenantAsync(f => f.Documento == documento).Result.Value.Any();

        return available ? Result.Ok() : Error.NullValue;
    }
    private Result EmailDisponivel(string email)
    {
        var available = !_unitOfWork.ONGRepository.SearchWithoutTenantAsync(f => f.Contato.Email.Endereco == email).Result.Value.Any();

        return available ? Result.Ok() : Error.NullValue;
    }
    private Result EhSuperAdmin()
    {
        if (AppUser.HasClaim("Permissions", "SuperAdmin"))
        {
            return Result.Ok();
        }
        return Error.NullValue;
    }
}