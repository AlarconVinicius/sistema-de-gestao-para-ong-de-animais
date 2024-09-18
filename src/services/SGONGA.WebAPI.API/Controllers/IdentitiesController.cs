using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGONGA.Core.Notifications;
using SGONGA.WebAPI.API.Controllers.Shared;
using SGONGA.WebAPI.Business.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Requests;

namespace SGONGA.WebAPI.API.Controllers;

[Authorize]
[Route("api/v1/identities/")]
public class IdentitiesController : ApiController
{
    public readonly IIdentityHandler _identityHandler;
    public IdentitiesController(INotifier notifier, IIdentityHandler identityHandler) : base(notifier)
    {
        _identityHandler = identityHandler;
    }

    #region Public Methods
    /// <summary>
    /// Realiza o login de um usuário.
    /// </summary>
    /// <remarks>
    /// Este endpoint permite que um usuário faça login no sistema.
    /// Não é necessário fornecer cabeçalhos de autenticação ou identificação de locatário para este endpoint.
    /// </remarks>
    /// <param name="request">Dados de login do usuário</param>
    /// <returns>Resposta com token de autenticação e detalhes do usuário</returns>
    /// <response code="200">Login realizado com sucesso</response>
    /// <response code="400">Retorna erros de validação</response>
    [AllowAnonymous]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ResponseBadRequest(ModelState);
        }

        var response = await _identityHandler.LoginAsync(request);

        return IsOperationValid() ? ResponseOk(response) : ResponseBadRequest();
    }

    /// <summary>
    /// Atualiza o e-mail do usuário.
    /// </summary>
    /// <remarks>
    /// Este endpoint permite atualizar o e-mail do usuário.
    /// Não é necessário fornecer cabeçalhos de autenticação ou identificação de locatário para este endpoint.
    /// </remarks>
    /// <param name="request">Dados de atualização do e-mail</param>
    /// <returns>Resposta indicando o sucesso ou falha na atualização do e-mail</returns>
    /// <response code="200">E-mail atualizado com sucesso</response>
    /// <response code="400">Retorna erros de validação</response>
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [HttpPut("atualizar/email")]
    public async Task<IActionResult> UpdateUserEmail(UpdateUserEmailRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ResponseBadRequest(ModelState);
        }
        await _identityHandler.UpdateEmailAsync(request);

        return IsOperationValid() ? ResponseOk() : ResponseBadRequest();
    }

    /// <summary>
    /// Atualiza a senha do usuário.
    /// </summary>
    /// <remarks>
    /// Este endpoint permite atualizar a senha do usuário.
    /// Não é necessário fornecer cabeçalhos de autenticação ou identificação de locatário para este endpoint.
    /// </remarks>
    /// <param name="request">Dados de atualização da senha</param>
    /// <returns>Resposta indicando o sucesso ou falha na atualização da senha</returns>
    /// <response code="200">Senha atualizada com sucesso</response>
    /// <response code="400">Retorna erros de validação</response>
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [HttpPut("atualizar/senha")]
    public async Task<IActionResult> UpdateUserPassword(UpdateUserPasswordRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ResponseBadRequest(ModelState);
        }
        await _identityHandler.UpdatePasswordAsync(request);

        return IsOperationValid() ? ResponseOk() : ResponseBadRequest();
    }
    #endregion

    #region Admin Methods
    /// <summary>
    /// Cadastra um novo usuário (SuperAdmin).
    /// </summary>
    /// <remarks>
    /// Este endpoint permite o cadastro de um novo usuário de forma administrativa.
    /// Para uso administrativo, é necessário fornecer o cabeçalho de autenticação apropriado:
    /// 
    /// - **Authorization**: Bearer Token para autenticação do usuário.
    /// 
    /// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    /// </remarks>
    /// <param name="request">Dados de cadastro do usuário</param>
    /// <returns>Resposta indicando o sucesso ou falha na criação do usuário</returns>
    /// <response code="200">Usuário criado com sucesso</response>
    /// <response code="400">Retorna erros relacionados à requisição</response>
    /// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    /// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    //[ClaimsAuthorize("Permissions", "SuperAdmin")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPost("cadastrar")]
    public async Task<IActionResult> Create(CreateUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ResponseBadRequest(ModelState);
        }
        var response = await _identityHandler.CreateAsync(request);

        return IsOperationValid() ? ResponseOk(response) : ResponseBadRequest();
    }


    /// <summary>
    /// Deleta um usuário (Admin).
    /// </summary>
    /// <remarks>
    /// Este endpoint permite a exclusão de um usuário específico de forma administrativa.
    /// Para uso administrativo, é necessário fornecer o cabeçalho de autenticação apropriado:
    /// 
    /// - **Authorization**: Bearer Token para autenticação do usuário.
    /// - **TenantId**: ID do locatário para identificar o ambiente.
    /// 
    /// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    /// O `TenantId` deve ser o identificador do locatário que está fazendo a requisição.
    /// </remarks>
    /// <param name="id">ID do usuário (GUID)</param>
    /// <returns>Resposta indicando o sucesso ou falha na exclusão do usuário</returns>
    /// <response code="200">Usuário deletado com sucesso</response>
    /// <response code="400">Retorna erros relacionados à requisição</response>
    /// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    /// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    //[ClaimsAuthorize("Permissions", "SuperAdmin")]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        DeleteUserRequest request = new(id);
        if (!ModelState.IsValid)
        {
            return ResponseBadRequest(ModelState);
        }
        await _identityHandler.DeleteAsync(request);

        return IsOperationValid() ? ResponseOk() : ResponseBadRequest();
    }

    ///// <summary>
    ///// Retorna as informações de um usuário pelo ID (SuperAdmin).
    ///// </summary>
    ///// <remarks>
    ///// Este endpoint permite recuperar as informações de um usuário específico.
    ///// Não é necessário fornecer cabeçalhos de autenticação ou identificação de locatário para este endpoint.
    ///// </remarks>
    ///// <param name="id">ID do usuário (GUID)</param>
    ///// <returns>Detalhes do usuário</returns>
    ///// <response code="200">Detalhes do usuário retornados com sucesso</response>
    ///// <response code="400">Retorna erros relacionados à requisição</response>
    ///// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    ///// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    //[ClaimsAuthorize("Permissions", "SuperAdmin")]
    //[ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //[ProducesResponseType(StatusCodes.Status403Forbidden)]
    //[HttpGet("{id:guid}")]
    //public async Task<IActionResult> GetById(Guid id)
    //{
    //    GetUserByIdRequest request = new(id);
    //    var result = await _identityHandler.GetByIdAsync(request);

    //    return IsOperationValid() ? ResponseOk(result) : ResponseBadRequest();

    //}

    ///// <summary>
    ///// Retorna todos os usuários (SuperAdmin).
    ///// </summary>
    ///// <remarks>
    ///// Este endpoint permite recuperar uma lista de todos os usuários de forma administrativa.
    ///// Para uso administrativo, é necessário fornecer o cabeçalho de autenticação apropriado:
    ///// 
    ///// - **Authorization**: Bearer Token para autenticação do usuário.
    ///// 
    ///// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    ///// </remarks>
    ///// <param name="ps">Tamanho da página para paginação (padrão: 25)</param>
    ///// <param name="page">Número da página para paginação (padrão: 1)</param>
    ///// <param name="q">Consulta para filtragem de usuários</param>
    ///// <returns>Lista de usuários</returns>
    ///// <response code="200">Lista de usuários retornada com sucesso</response>
    ///// <response code="400">Retorna erros relacionados à requisição</response>
    ///// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    ///// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    //[ClaimsAuthorize("Permissions", "SuperAdmin")]
    //[ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //[ProducesResponseType(StatusCodes.Status403Forbidden)]
    //[HttpGet]
    //public async Task<IActionResult> GetAll([FromQuery] int ps = ConfigurationDefault.DefaultPageSize, [FromQuery] int page = ConfigurationDefault.DefaultPageNumber, [FromQuery] string q = null!)
    //{
    //    GetAllUsersRequest request = new()
    //    {
    //        PageSize = ps,
    //        PageNumber = page,
    //        Query = q
    //    };
    //    var result = await _identityHandler.GetAllAsync(request);

    //    return IsOperationValid() ? ResponseOk(result) : ResponseBadRequest();
    //}
    #endregion
}
