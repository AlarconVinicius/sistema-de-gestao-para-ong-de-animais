using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGONGA.Core.Notifications;
using SGONGA.WebAPI.API.Controllers.Shared;
using SGONGA.WebAPI.Business.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Requests;

namespace SGONGA.WebAPI.API.Controllers;

[Authorize]
[Route("api/v1/colaboradores/admin/")]
public class ColaboradoresController : ApiController
{
    public readonly IColaboradorHandler _colaboradorHandler;
    public ColaboradoresController(INotifier notifier, IColaboradorHandler colaboradorHandler) : base(notifier)
    {
        _colaboradorHandler = colaboradorHandler;
    }

    #region Admin Methods

    /// <summary>
    /// Retorna as informações de um colaborador pelo ID (Admin).
    /// </summary>
    /// <remarks>
    /// Este endpoint permite recuperar informações detalhadas sobre um colaborador específico.
    /// Para uso administrativo, é necessário fornecer o cabeçalho de autenticação apropriado:
    /// 
    /// - **Authorization**: Bearer Token para autenticação do usuário.
    /// 
    /// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    /// </remarks>
    /// <param name="id">ID do colaborador (GUID)</param>
    /// <returns>Detalhes do colaborador</returns>
    /// <response code="200">Detalhes do colaborador retornados com sucesso</response>
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
        GetColaboradorByIdRequest request = new(id);
        var result = await _colaboradorHandler.GetByIdAsync(request);

        return IsOperationValid() ? ResponseOk(result) : ResponseBadRequest();

    }

    ///// <summary>
    ///// Retorna todos os colaboradores (Admin).
    ///// </summary>
    ///// <remarks>
    ///// Este endpoint permite recuperar uma lista de todos os colaboradores de forma administrativa.
    ///// Para uso administrativo, é necessário fornecer o cabeçalho de autenticação apropriado:
    ///// 
    ///// - **Authorization**: Bearer Token para autenticação do usuário.
    ///// 
    ///// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    ///// </remarks>
    ///// <param name="ps">Tamanho da página para paginação (padrão: 25)</param>
    ///// <param name="page">Número da página para paginação (padrão: 1)</param>
    ///// <param name="q">Consulta para filtragem de colaboradores</param>
    ///// <returns>Lista de colaboradores</returns>
    ///// <response code="200">Lista de colaboradores retornada com sucesso</response>
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
    //    GetAllColaboradoresRequest request = new()
    //    {
    //        PageSize = ps,
    //        PageNumber = page,
    //        Query = q
    //    };
    //    var result = await _colaboradorHandler.GetAllAsync(request);

    //    return IsOperationValid() ? ResponseOk(result) : ResponseBadRequest();
    //}

    ///// <summary>
    ///// Cria um novo colaborador (Admin).
    ///// </summary>
    ///// <remarks>
    ///// Este endpoint permite o cadastro de um novo colaborador de forma administrativa.
    ///// Para uso administrativo, é necessário fornecer o cabeçalho de autenticação apropriado:
    ///// 
    ///// - **Authorization**: Bearer Token para autenticação do usuário.
    ///// 
    ///// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    ///// </remarks>
    ///// <param name="request">Dados de criação do colaborador</param>
    ///// <returns>Resposta indicando o sucesso ou falha na criação do colaborador</returns>
    ///// <response code="201">Colaborador criado com sucesso</response>
    ///// <response code="400">Retorna erros de validação</response>
    ///// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    ///// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    //[ClaimsAuthorize("Permissions", "SuperAdmin")]
    //[ProducesResponseType(typeof(CustomResult), StatusCodes.Status201Created)]
    //[ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //[ProducesResponseType(StatusCodes.Status403Forbidden)]
    //[HttpPost]
    //public async Task<IActionResult> Post(CreateColaboradorRequest request)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return ResponseBadRequest(ModelState);
    //    }
    //    await _colaboradorHandler.CreateAsync(request);

    //    return IsOperationValid() ? ResponseOk(request) : ResponseBadRequest();
    //}

    /// <summary>
    /// Atualiza um colaborador existente (Admin).
    /// </summary>
    /// <remarks>
    /// Este endpoint permite a atualização dos dados de um colaborador específico de forma administrativa.
    /// Para uso administrativo, é necessário fornecer o cabeçalho de autenticação apropriado:
    /// 
    /// - **Authorization**: Bearer Token para autenticação do usuário.
    /// 
    /// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    /// </remarks>
    /// <param name="id">ID do colaborador (GUID)</param>
    /// <param name="request">Dados de atualização do colaborador</param>
    /// <returns>Resposta indicando o sucesso ou falha na atualização do colaborador</returns>
    /// <response code="200">Colaborador atualizado com sucesso</response>
    /// <response code="400">Retorna erros de validação</response>
    /// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    /// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateColaboradorRequest request)
    {
        if (id != request.Id) return ResponseBadRequest("Os IDs não correspondem.");
        if (!ModelState.IsValid)
        {
            return ResponseBadRequest(ModelState);
        }

        await _colaboradorHandler.UpdateAsync(request);

        return IsOperationValid() ? ResponseOk() : ResponseBadRequest();
    }

    ///// <summary>
    ///// Deleta um colaborador existente (Admin).
    ///// </summary>
    ///// <remarks>
    ///// Este endpoint permite a exclusão de um colaborador específico de forma administrativa.
    ///// Para uso administrativo, é necessário fornecer o cabeçalho de autenticação apropriado:
    ///// 
    ///// - **Authorization**: Bearer Token para autenticação do usuário.
    ///// 
    ///// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    ///// </remarks>
    ///// <param name="id">ID do colaborador (GUID)</param>
    ///// <returns>Resposta indicando o sucesso ou falha na exclusão do colaborador</returns>
    ///// <response code="200">Colaborador deletado com sucesso</response>
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
    //    await _colaboradorHandler.DeleteAsync(new DeleteColaboradorRequest(id));

    //    return IsOperationValid() ? ResponseOk() : ResponseBadRequest();
    //}
    #endregion
}