using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGONGA.Core.Configurations;
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
    [AllowAnonymous]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
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
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        GetSolicitacaoCadastroByIdRequest request = new(id);
        var result = await _solicitacaoCadastroHandler.GetByIdAsync(request);

        return IsOperationValid() ? ResponseOk(result) : ResponseBadRequest();

    }

    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
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

    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
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
