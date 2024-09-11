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
[Route("api/v1/solicitacoes-cadastro/admin/")]
public class SolicitacoesCadastroController : ApiController
{
    public readonly ISolicitacaoCadastroHandler _solicitacaoCadastroHandler;
    public SolicitacoesCadastroController(INotifier notifier, ISolicitacaoCadastroHandler solicitacaoCadastroHandler) : base(notifier)
    {
        _solicitacaoCadastroHandler = solicitacaoCadastroHandler;
    }

    #region Public Methods

    /// <summary>
    /// Solicita um novo cadastro.
    /// </summary>
    /// <remarks>
    /// Este endpoint permite a criação de uma nova solicitação de cadastro.
    /// Não é necessário fornecer cabeçalhos de autenticação ou identificação de locatário para este endpoint.
    /// </remarks>
    /// <param name="request">Dados da solicitação de cadastro</param>
    /// <returns>Resposta indicando o sucesso ou falha na criação da solicitação</returns>
    /// <response code="201">Solicitação criada com sucesso</response>
    /// <response code="400">Retorna erros de validação</response>
    [AllowAnonymous]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [HttpPost("/api/v1/solicitacoes-cadastro")]
    public async Task<IActionResult> Post(CreateSolicitacaoCadastroRequests request)
    {
        if (!ModelState.IsValid)
        {
            return ResponseBadRequest(ModelState);
        }
        await _solicitacaoCadastroHandler.RequestRegistrationAsync(request);

        return IsOperationValid() ? ResponseCreated() : ResponseBadRequest();
    }
    #endregion

    #region Admin Methods

    /// <summary>
    /// Retorna as informações de uma solicitação de cadastro pelo ID (SuperAdmin).
    /// </summary>
    /// <remarks>
    /// Este endpoint permite recuperar as informações de uma solicitação de cadastro específica de forma administrativa.
    /// Para uso administrativo, é necessário fornecer o cabeçalho de autenticação apropriado:
    /// 
    /// - **Authorization**: Bearer Token para autenticação do usuário.
    /// 
    /// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    /// </remarks>
    /// <param name="id">ID da solicitação de cadastro (GUID)</param>
    /// <returns>Detalhes da solicitação</returns>
    /// <response code="200">Detalhes da solicitação retornados com sucesso</response>
    /// <response code="400">Retorna erros relacionados à requisição</response>
    /// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    /// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    [ClaimsAuthorize("Permissions", "SuperAdmin")]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        GetSolicitacaoCadastroByIdRequest request = new(id);
        var result = await _solicitacaoCadastroHandler.GetByIdAsync(request);

        return IsOperationValid() ? ResponseOk(result) : ResponseBadRequest();

    }

    /// <summary>
    /// Retorna todas as solicitações de cadastro (SuperAdmin).
    /// </summary>
    /// <remarks>
    /// Este endpoint permite recuperar uma lista de todas as solicitações de cadastro de forma administrativa.
    /// Para uso administrativo, é necessário fornecer o cabeçalho de autenticação apropriado:
    /// 
    /// - **Authorization**: Bearer Token para autenticação do usuário.
    /// 
    /// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    /// </remarks>
    /// <param name="ps">Tamanho da página para paginação (padrão: 25)</param>
    /// <param name="page">Número da página para paginação (padrão: 1)</param>
    /// <param name="q">Consulta para filtragem de solicitações</param>
    /// <returns>Lista de solicitações de cadastro</returns>
    /// <response code="200">Lista de solicitações retornada com sucesso</response>
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
        GetAllSolicitacoesCadastroRequest request = new()
        {
            PageSize = ps,
            PageNumber = page,
            Query = q
        };
        var result = await _solicitacaoCadastroHandler.GetAllAsync(request);

        return IsOperationValid() ? ResponseOk(result) : ResponseBadRequest();
    }

    /// <summary>
    /// Atualiza o status de uma solicitação de cadastro (SuperAdmin).
    /// </summary>
    /// <remarks>
    /// Este endpoint permite a atualização do status de uma solicitação de cadastro.
    /// Para uso administrativo, é necessário fornecer o cabeçalho de autenticação apropriado:
    /// 
    /// - **Authorization**: Bearer Token para autenticação do usuário.
    /// 
    /// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    /// </remarks>
    /// <param name="id">ID da solicitação de cadastro (GUID)</param>
    /// <param name="request">Dados atualizados da solicitação</param>
    /// <returns>Resposta indicando o sucesso ou falha na atualização da solicitação</returns>
    /// <response code="200">Solicitação atualizada com sucesso</response>
    /// <response code="400">Retorna erros relacionados à requisição</response>
    /// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    /// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    [ClaimsAuthorize("Permissions", "SuperAdmin")]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateStatusSolicitacaoCadastroRequest request)
    {
        if (id != request.Id) return ResponseBadRequest("Os IDs não correspondem.");
        if (!ModelState.IsValid)
        {
            return ResponseBadRequest(ModelState);
        }

        await _solicitacaoCadastroHandler.UpdateStatusRequestRegistrationAsync(request);

        return IsOperationValid() ? ResponseOk() : ResponseBadRequest();
    }
    #endregion
}
