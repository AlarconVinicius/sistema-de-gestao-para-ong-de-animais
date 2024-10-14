using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGONGA.Core.Configurations;
using SGONGA.Core.Notifications;
using SGONGA.WebAPI.API.Animals.Command.CreateAnimal;
using SGONGA.WebAPI.API.Controllers.Shared;
using SGONGA.WebAPI.API.Extensions;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;

namespace SGONGA.WebAPI.API.Controllers;

[Authorize]
[Route("api/v1/animais/admin/")]
public class AnimalsController : ApiController
{
    public readonly IAnimalHandler _animalHandler;
    public AnimalsController(INotifier notifier, IAnimalHandler animalHandler, ISender sender) : base(notifier, sender)
    {
        _animalHandler = animalHandler;
    }

    #region Public Methods

    /// <summary>
    /// Retorna as informações de um animal público pelo ID.
    /// </summary>
    /// <remarks>
    /// Este endpoint permite recuperar as informações de um animal específico de forma pública.
    /// Não é necessário fornecer cabeçalhos de autenticação ou identificação de locatário para este endpoint.
    /// </remarks>
    /// <param name="id">ID do animal (GUID)</param>
    /// <returns>Detalhes do animal</returns>
    /// <response code="200">Detalhes do animal retornados com sucesso</response>
    /// <response code="400">Retorna erros relacionados à requisição</response>
    [AllowAnonymous]
    [ProducesResponseType(typeof(AnimalResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("/api/v1/animais/{id:guid}")]
    public async Task<IResult> GetByIdPublic(Guid id)
    {
        GetAnimalByIdRequest request = new(id, false);
        var result = await _animalHandler.GetByIdAsync(request);

        return result.Match(
            onSuccess: response => Results.Ok(response),
            onFailure: response => response.ToProblemDetails());
    }

    /// <summary>
    /// Retorna todos os animais públicos.
    /// </summary>
    /// <remarks>
    /// Este endpoint permite recuperar uma lista de todos os animais de forma pública.
    /// Não é necessário fornecer cabeçalhos de autenticação ou identificação de locatário para este endpoint.
    /// </remarks>
    /// <param name="ps">Tamanho da página para paginação (padrão: 25)</param>
    /// <param name="page">Número da página para paginação (padrão: 1)</param>
    /// <param name="q">Consulta para filtragem de animais</param>
    /// <returns>Lista de animais</returns>
    /// <response code="200">Lista de animais retornada com sucesso</response>
    /// <response code="400">Retorna erros relacionados à requisição</response>
    [AllowAnonymous]
    [ProducesResponseType(typeof(PagedResponse<AnimalResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("/api/v1/animais")]
    public async Task<IResult> GetAllPublic([FromQuery] int ps = ConfigurationDefault.DefaultPageSize, [FromQuery] int page = ConfigurationDefault.DefaultPageNumber, [FromQuery] string q = null!)
    {
        GetAllAnimaisRequest request = new()
        {
            PageSize = ps,
            PageNumber = page,
            Query = q,
            TenantFiltro = false
        };
        var result = await _animalHandler.GetAllAsync(request);

        return result.Match(
            onSuccess: response => Results.Ok(response),
            onFailure: response => response.ToProblemDetails());
    }
    #endregion

    #region Admin Methods

    /// <summary>
    /// Retorna as informações de um animal pelo ID (Admin).
    /// </summary>
    /// <remarks>
    /// Este endpoint permite recuperar as informações de um animal específico de forma administrativa.
    /// Para uso administrativo, é necessário fornecer os seguintes cabeçalhos na requisição:
    /// 
    /// - **Authorization**: Bearer Token para autenticação do usuário.
    /// - **TenantId**: ID do locatário para identificar o ambiente.
    /// 
    /// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    /// O `TenantId` deve ser o identificador do locatário que está fazendo a requisição.
    /// </remarks>
    /// <param name="id">ID do animal (GUID)</param>
    /// <returns>Detalhes do animal</returns>
    /// <response code="200">Detalhes do animal retornados com sucesso</response>
    /// <response code="400">Retorna erros relacionados à requisição</response>
    /// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    /// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    [ProducesResponseType(typeof(AnimalResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpGet("{id:guid}")]
    public async Task<IResult> GetById(Guid id)
    {
        GetAnimalByIdRequest request = new(id, true);
        var result = await _animalHandler.GetByIdAsync(request);

        return result.Match(
            onSuccess: response => Results.Ok(response),
            onFailure: response => response.ToProblemDetails());

    }

    /// <summary>
    /// Retorna todos os animais (Admin).
    /// </summary>
    /// <remarks>
    /// Este endpoint permite recuperar uma lista de todos os animais de forma administrativa.
    /// Para uso administrativo, é necessário fornecer os seguintes cabeçalhos na requisição:
    /// 
    /// - **Authorization**: Bearer Token para autenticação do usuário.
    /// - **TenantId**: ID do locatário para identificar o ambiente.
    /// 
    /// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    /// O `TenantId` deve ser o identificador do locatário que está fazendo a requisição.
    /// </remarks>
    /// <param name="ps">Tamanho da página para paginação (padrão: 25)</param>
    /// <param name="page">Número da página para paginação (padrão: 1)</param>
    /// <param name="q">Consulta para filtragem de animais</param>
    /// <returns>Lista de animais</returns>
    /// <response code="200">Lista de animais retornada com sucesso</response>
    /// <response code="400">Retorna erros relacionados à requisição</response>
    /// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    /// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    [ProducesResponseType(typeof(PagedResponse<AnimalResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpGet]
    public async Task<IResult> GetAll([FromQuery] int ps = ConfigurationDefault.DefaultPageSize, [FromQuery] int page = ConfigurationDefault.DefaultPageNumber, [FromQuery] string q = null!)
    {
        GetAllAnimaisRequest request = new()
        {
            PageSize = ps,
            PageNumber = page,
            Query = q,
            TenantFiltro = true
        };
        var result = await _animalHandler.GetAllAsync(request);

        return result.Match(
            onSuccess: response => Results.Ok(response),
            onFailure: response => response.ToProblemDetails());
    }

    /// <summary>
    /// Cadastro de animal (Admin).
    /// </summary>
    /// <remarks>
    /// Este endpoint permite o cadastro de um novo animal. 
    /// Para uso administrativo, é necessário fornecer os seguintes cabeçalhos na requisição:
    /// 
    /// - **Authorization**: Bearer Token para autenticação do usuário.
    /// - **TenantId**: ID do locatário para identificar o ambiente.
    /// 
    /// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    /// O `TenantId` deve ser o identificador do locatário que está fazendo a requisição.
    /// </remarks>
    /// <param name="command">Dados de cadastro do animal</param>
    /// <param name="cancellationToken">Dados de cadastro do animal</param>
    /// <returns>Resposta indicando o sucesso ou falha na criação do animal</returns>
    /// <response code="201">Animal criado com sucesso</response>
    /// <response code="400">Retorna erros de validação</response>
    /// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    /// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPost]
    public async Task<IResult> Post(CreateAnimalCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.Match(
            onSuccess: () => Results.Created(),
            onFailure: response => response.ToProblemDetails());
    }

    /// <summary>
    /// Atualização de animal (Admin).
    /// </summary>
    /// <remarks>
    /// Este endpoint permite a atualização dos dados de um animal. 
    /// Para uso administrativo, é necessário fornecer os seguintes cabeçalhos na requisição:
    /// 
    /// - **Authorization**: Bearer Token para autenticação do usuário.
    /// - **TenantId**: ID do locatário para identificar o ambiente.
    /// 
    /// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    /// O `TenantId` deve ser o identificador do locatário que está fazendo a requisição.
    /// </remarks>
    /// <param name="id">ID do animal (GUID)</param>
    /// <param name="request">Dados atualizados do animal</param>
    /// <returns>Resposta indicando o sucesso ou falha na atualização do animal</returns>
    /// <response code="204">Animal atualizado com sucesso</response>
    /// <response code="400">Retorna erros de validação ou IDs não correspondentes</response>
    /// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    /// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPut("{id:guid}")]
    public async Task<IResult> Update(Guid id, UpdateAnimalRequest request)
    {
        if (id != request.Id) 
            ModelState.AddModelError(string.Empty, "Os IDs não correspondem.");

        if (!ModelState.IsValid)
        {
            return ModelState.ToProblemDetails();
        }

        var result = await _animalHandler.UpdateAsync(request);

        return result.Match(
            onSuccess: () => Results.NoContent(),
            onFailure: response => response.ToProblemDetails());
    }

    /// <summary>
    /// Deleta um animal (Admin).
    /// </summary>
    /// <remarks>
    /// Este endpoint permite a exclusão de um animal específico. 
    /// Para uso administrativo, é necessário fornecer os seguintes cabeçalhos na requisição:
    /// 
    /// - **Authorization**: Bearer Token para autenticação do usuário.
    /// - **TenantId**: ID do locatário para identificar o ambiente.
    /// 
    /// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    /// O `TenantId` deve ser o identificador do locatário que está fazendo a requisição.
    /// </remarks>
    /// <param name="id">ID do animal (GUID)</param>
    /// <returns>Resposta indicando o sucesso ou falha na exclusão do animal</returns>
    /// <response code="204">Animal deletado com sucesso</response>
    /// <response code="400">Retorna erros relacionados à requisição</response>
    /// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    /// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpDelete("{id:guid}")]
    public async Task<IResult> Delete(Guid id)
    {
        var result = await _animalHandler.DeleteAsync(new DeleteAnimalRequest(id));

        return result.Match(
            onSuccess: () => Results.NoContent(),
            onFailure: response => response.ToProblemDetails());
    }
    #endregion
}
