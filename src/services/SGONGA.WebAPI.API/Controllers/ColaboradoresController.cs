using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGONGA.Core.Notifications;
using SGONGA.WebAPI.API.Controllers.Shared;
using SGONGA.WebAPI.Business.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Requests;

namespace SGONGA.WebAPI.API.Controllers;

[Authorize]
[Route("api/v1/adotantes/admin/")]
public class AdotantesController : ApiController
{
    public readonly IAdotanteHandler _adotanteHandler;
    public AdotantesController(INotifier notifier, IAdotanteHandler adotanteHandler) : base(notifier)
    {
        _adotanteHandler = adotanteHandler;
    }

    #region Admin Methods

    /// <summary>
    /// Retorna as informações de um adotante pelo ID (Admin).
    /// </summary>
    /// <remarks>
    /// Este endpoint permite recuperar informações detalhadas sobre um adotante específico.
    /// Para uso administrativo, é necessário fornecer o cabeçalho de autenticação apropriado:
    /// 
    /// - **Authorization**: Bearer Token para autenticação do usuário.
    /// 
    /// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    /// </remarks>
    /// <param name="id">ID do adotante (GUID)</param>
    /// <returns>Detalhes do adotante</returns>
    /// <response code="200">Detalhes do adotante retornados com sucesso</response>
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
        GetAdotanteByIdRequest request = new(id);
        var result = await _adotanteHandler.GetByIdAsync(request);

        return IsOperationValid() ? ResponseOk(result) : ResponseBadRequest();

    }

    ///// <summary>
    ///// Retorna todos os adotantes (Admin).
    ///// </summary>
    ///// <remarks>
    ///// Este endpoint permite recuperar uma lista de todos os adotantes de forma administrativa.
    ///// Para uso administrativo, é necessário fornecer o cabeçalho de autenticação apropriado:
    ///// 
    ///// - **Authorization**: Bearer Token para autenticação do usuário.
    ///// 
    ///// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    ///// </remarks>
    ///// <param name="ps">Tamanho da página para paginação (padrão: 25)</param>
    ///// <param name="page">Número da página para paginação (padrão: 1)</param>
    ///// <param name="q">Consulta para filtragem de adotantes</param>
    ///// <returns>Lista de adotantes</returns>
    ///// <response code="200">Lista de adotantes retornada com sucesso</response>
    ///// <response code="400">Retorna erros relacionados à requisição</response>
    ///// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    ///// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    //[ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //[ProducesResponseType(StatusCodes.Status403Forbidden)]
    //[HttpGet]
    //public async Task<IActionResult> GetAll([FromQuery] int ps = ConfigurationDefault.DefaultPageSize, [FromQuery] int page = ConfigurationDefault.DefaultPageNumber, [FromQuery] string q = null!)
    //{
    //    GetAllAdotantesRequest request = new()
    //    {
    //        PageSize = ps,
    //        PageNumber = page,
    //        Query = q
    //    };
    //    var result = await _adotanteHandler.GetAllAsync(request);

    //    return IsOperationValid() ? ResponseOk(result) : ResponseBadRequest();
    //}

    ///// <summary>
    ///// Cria um novo adotante (Admin).
    ///// </summary>
    ///// <remarks>
    ///// Este endpoint permite o cadastro de um novo adotante de forma administrativa.
    ///// Para uso administrativo, é necessário fornecer o cabeçalho de autenticação apropriado:
    ///// 
    ///// - **Authorization**: Bearer Token para autenticação do usuário.
    ///// 
    ///// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    ///// </remarks>
    ///// <param name="request">Dados de criação do adotante</param>
    ///// <returns>Resposta indicando o sucesso ou falha na criação do adotante</returns>
    ///// <response code="201">Adotante criado com sucesso</response>
    ///// <response code="400">Retorna erros de validação</response>
    ///// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    ///// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    //[ClaimsAuthorize("Permissions", "SuperAdmin")]
    //[ProducesResponseType(typeof(CustomResult), StatusCodes.Status201Created)]
    //[ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //[ProducesResponseType(StatusCodes.Status403Forbidden)]
    //[HttpPost]
    //public async Task<IActionResult> Post(CreateAdotanteRequest request)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return ResponseBadRequest(ModelState);
    //    }
    //    await _adotanteHandler.CreateAsync(request);

    //    return IsOperationValid() ? ResponseOk(request) : ResponseBadRequest();
    //}

    /// <summary>
    /// Atualiza um adotante existente (Admin).
    /// </summary>
    /// <remarks>
    /// Este endpoint permite a atualização dos dados de um adotante específico de forma administrativa.
    /// Para uso administrativo, é necessário fornecer o cabeçalho de autenticação apropriado:
    /// 
    /// - **Authorization**: Bearer Token para autenticação do usuário.
    /// 
    /// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    /// </remarks>
    /// <param name="id">ID do adotante (GUID)</param>
    /// <param name="request">Dados de atualização do adotante</param>
    /// <returns>Resposta indicando o sucesso ou falha na atualização do adotante</returns>
    /// <response code="200">Adotante atualizado com sucesso</response>
    /// <response code="400">Retorna erros de validação</response>
    /// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    /// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateAdotanteRequest request)
    {
        if (id != request.Id) return ResponseBadRequest("Os IDs não correspondem.");
        if (!ModelState.IsValid)
        {
            return ResponseBadRequest(ModelState);
        }

        await _adotanteHandler.UpdateAsync(request);

        return IsOperationValid() ? ResponseOk() : ResponseBadRequest();
    }

    ///// <summary>
    ///// Deleta um adotante existente (Admin).
    ///// </summary>
    ///// <remarks>
    ///// Este endpoint permite a exclusão de um adotante específico de forma administrativa.
    ///// Para uso administrativo, é necessário fornecer o cabeçalho de autenticação apropriado:
    ///// 
    ///// - **Authorization**: Bearer Token para autenticação do usuário.
    ///// 
    ///// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    ///// </remarks>
    ///// <param name="id">ID do adotante (GUID)</param>
    ///// <returns>Resposta indicando o sucesso ou falha na exclusão do adotante</returns>
    ///// <response code="200">Adotante deletado com sucesso</response>
    ///// <response code="400">Retorna erros relacionados à requisição</response>
    ///// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    ///// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    //[ClaimsAuthorize("Permissions", "SuperAdmin")]
    //[ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //[ProducesResponseType(StatusCodes.Status403Forbidden)]
    //[HttpDelete("{id:guid}")]
    //public async Task<IActionResult> Delete(Guid id)
    //{
    //    await _adotanteHandler.DeleteAsync(new DeleteAdotanteRequest(id));

    //    return IsOperationValid() ? ResponseOk() : ResponseBadRequest();
    //}
    #endregion
}