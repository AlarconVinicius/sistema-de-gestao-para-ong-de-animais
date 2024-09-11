using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGONGA.Core.Configurations;
using SGONGA.Core.Extensions;
using SGONGA.Core.Notifications;
using SGONGA.WebAPI.API.Controllers.Shared;
using SGONGA.WebAPI.Business.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Requests;

namespace SGONGA.WebAPI.API.Controllers;

[Authorize]
[Route("api/v1/ongs/admin/")]
public class ONGsController : ApiController
{
    public readonly IONGHandler _ongHandler;
    public ONGsController(INotifier notifier, IONGHandler ongHandler) : base(notifier)
    {
        _ongHandler = ongHandler;
    }

    #region Public Methods

    /// <summary>
    /// Retorna as informações de uma ONG pública pelo ID.
    /// </summary>
    /// <remarks>
    /// Este endpoint permite recuperar informações de uma ONG específica.
    /// Não é necessário fornecer cabeçalhos de autenticação para este endpoint.
    /// </remarks>
    /// <param name="id">ID da ONG (GUID)</param>
    /// <returns>Detalhes da ONG</returns>
    /// <response code="200">Detalhes da ONG retornados com sucesso</response>
    /// <response code="400">Retorna erros relacionados à requisição</response>
    [AllowAnonymous]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [HttpGet("/api/v1/ongs/{id:guid}")]
    public async Task<IActionResult> GetByIdPublic(Guid id)
    {
        GetONGByIdRequest request = new(id, false);
        var result = await _ongHandler.GetByIdAsync(request);

        return IsOperationValid() ? ResponseOk(result) : ResponseBadRequest();

    }

    /// <summary>
    /// Retorna todas as ONGs públicas.
    /// </summary>
    /// <remarks>
    /// Este endpoint permite recuperar uma lista de todas as ONGs.
    /// Não é necessário fornecer cabeçalhos de autenticação para este endpoint.
    /// </remarks>
    /// <param name="ps">Tamanho da página para paginação (padrão: 25)</param>
    /// <param name="page">Número da página para paginação (padrão: 1)</param>
    /// <param name="q">Consulta para filtragem de ONGs</param>
    /// <returns>Lista de ONGs</returns>
    /// <response code="200">Lista de ONGs retornada com sucesso</response>
    /// <response code="400">Retorna erros relacionados à requisição</response>
    [AllowAnonymous]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [HttpGet("/api/v1/ongs")]
    public async Task<IActionResult> GetAllPublic([FromQuery] int ps = ConfigurationDefault.DefaultPageSize, [FromQuery] int page = ConfigurationDefault.DefaultPageNumber, [FromQuery] string q = null!)
    {
        GetAllONGsRequest request = new()
        {
            PageSize = ps,
            PageNumber = page,
            Query = q,
            TenantFiltro = false
        };
        var result = await _ongHandler.GetAllAsync(request);

        return IsOperationValid() ? ResponseOk(result) : ResponseBadRequest();
    }
    #endregion

    #region Admin Methods

    /// <summary>
    /// Retorna as informações de uma ONG pelo ID (Admin).
    /// </summary>
    /// <remarks>
    /// Este endpoint permite recuperar informações de uma ONG específica de forma administrativa.
    /// Para uso administrativo, é necessário fornecer o cabeçalho de autenticação apropriado:
    /// 
    /// - **Authorization**: Bearer Token para autenticação do usuário.
    /// - **TenantId**: ID do locatário para identificar o ambiente.
    /// 
    /// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    /// O `TenantId` deve ser o identificador do locatário que está fazendo a requisição.
    /// </remarks>
    /// <param name="id">ID da ONG (GUID)</param>
    /// <returns>Detalhes da ONG</returns>
    /// <response code="200">Detalhes da ONG retornados com sucesso</response>
    /// <response code="400">Retorna erros relacionados à requisição</response>
    /// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    /// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        GetONGByIdRequest request = new(id, true);
        var result = await _ongHandler.GetByIdAsync(request);

        return IsOperationValid() ? ResponseOk(result) : ResponseBadRequest();

    }

    /// <summary>
    /// Retorna todas as ONGs (SuperAdmin).
    /// </summary>
    /// <remarks>
    /// Este endpoint permite recuperar uma lista de todas as ONGs de forma administrativa.
    /// Para uso administrativo, é necessário fornecer o cabeçalho de autenticação apropriado:
    /// 
    /// - **Authorization**: Bearer Token para autenticação do usuário.
    /// 
    /// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    /// </remarks>
    /// <param name="ps">Tamanho da página para paginação (padrão: 25)</param>
    /// <param name="page">Número da página para paginação (padrão: 1)</param>
    /// <param name="q">Consulta para filtragem de ONGs</param>
    /// <returns>Lista de ONGs</returns>
    /// <response code="200">Lista de ONGs retornada com sucesso</response>
    /// <response code="400">Retorna erros relacionados à requisição</response>
    /// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    /// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    [ClaimsAuthorize("Permissions", "SuperAdmin")]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int ps = ConfigurationDefault.DefaultPageSize, [FromQuery] int page = ConfigurationDefault.DefaultPageNumber, [FromQuery] string q = null!)
    {
        GetAllONGsRequest request = new()
        {
            PageSize = ps,
            PageNumber = page,
            Query = q,
            TenantFiltro = true
        };
        var result = await _ongHandler.GetAllAsync(request);

        return IsOperationValid() ? ResponseOk(result) : ResponseBadRequest();
    }

    ///// <summary>
    ///// Cria uma nova ONG (SuperAdmin).
    ///// </summary>
    ///// <remarks>
    ///// Este endpoint permite o cadastro de uma nova ONG de forma administrativa.
    ///// Para uso administrativo, é necessário fornecer o cabeçalho de autenticação apropriado:
    ///// 
    ///// - **Authorization**: Bearer Token para autenticação do usuário.
    ///// 
    ///// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    ///// </remarks>
    ///// <param name="request">Dados de criação da ONG</param>
    ///// <returns>Resposta indicando o sucesso ou falha na criação da ONG</returns>
    ///// <response code="201">ONG criada com sucesso</response>
    ///// <response code="400">Retorna erros de validação</response>
    ///// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    ///// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    //[ClaimsAuthorize("Permissions", "SuperAdmin")]
    //[ProducesResponseType(typeof(CustomResult), StatusCodes.Status201Created)]
    //[ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //[ProducesResponseType(StatusCodes.Status403Forbidden)]
    //[HttpPost]
    //public async Task<IActionResult> Post(CreateONGRequest request)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return ResponseBadRequest(ModelState);
    //    }
    //    await _ongHandler.CreateAsync(request);

    //    return IsOperationValid() ? ResponseCreated() : ResponseBadRequest();
    //}

    /// <summary>
    /// Atualiza uma ONG existente (Admin).
    /// </summary>
    /// <remarks>
    /// Este endpoint permite a atualização dos dados de uma ONG específica de forma administrativa.
    /// Para uso administrativo, é necessário fornecer o cabeçalho de autenticação apropriado:
    /// 
    /// - **Authorization**: Bearer Token para autenticação do usuário.
    /// - **TenantId**: ID do locatário para identificar o ambiente.
    /// 
    /// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    /// O `TenantId` deve ser o identificador do locatário que está fazendo a requisição.
    /// </remarks>
    /// <param name="id">ID da ONG (GUID)</param>
    /// <param name="request">Dados de atualização da ONG</param>
    /// <returns>Resposta indicando o sucesso ou falha na atualização da ONG</returns>
    /// <response code="200">ONG atualizada com sucesso</response>
    /// <response code="400">Retorna erros de validação</response>
    /// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    /// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateONGRequest request)
    {
        if (id != request.Id) return ResponseBadRequest("Os IDs não correspondem.");
        if (!ModelState.IsValid)
        {
            return ResponseBadRequest(ModelState);
        }

        await _ongHandler.UpdateAsync(request);

        return IsOperationValid() ? ResponseOk() : ResponseBadRequest();
    }

    /// <summary>
    /// Deleta uma ONG existente (SuperAdmin).
    /// </summary>
    /// <remarks>
    /// Este endpoint permite a exclusão de uma ONG específica de forma administrativa.
    /// Para uso administrativo, é necessário fornecer o cabeçalho de autenticação apropriado:
    /// 
    /// - **Authorization**: Bearer Token para autenticação do usuário.
    /// 
    /// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    /// </remarks>
    /// <param name="id">ID da ONG (GUID)</param>
    /// <returns>Resposta indicando o sucesso ou falha na exclusão da ONG</returns>
    /// <response code="200">ONG deletada com sucesso</response>
    /// <response code="400">Retorna erros relacionados à requisição</response>
    /// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    /// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    [ClaimsAuthorize("Permissions", "SuperAdmin")]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _ongHandler.DeleteAsync(new DeleteONGRequest(id));

        return IsOperationValid() ? ResponseOk() : ResponseBadRequest();
    }
    #endregion
}
