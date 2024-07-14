using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGONGA.Core.Configurations;
using SGONGA.Core.Notifications;
using SGONGA.WebAPI.API.Controllers.Shared;
using SGONGA.WebAPI.Business.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Requests;

namespace SGONGA.WebAPI.API.Controllers;

[Route("api/v1/ongs/")]
public class ONGsController : ApiController
{
    public readonly IONGHandler _ongHandler;
    public ONGsController(INotifier notifier, IONGHandler ongHandler) : base(notifier)
    {
        _ongHandler = ongHandler;
    }

    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        GetONGByIdRequest request = new(id);
        var result = await _ongHandler.GetByIdAsync(request);

        return IsOperationValid() ? ResponseOk(result) : ResponseBadRequest();

    }

    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int ps = ConfigurationDefault.DefaultPageSize, [FromQuery] int page = ConfigurationDefault.DefaultPageNumber, [FromQuery] string q = null)
    {
        GetAllONGsRequest request = new()
        {
            PageSize = ps,
            PageNumber = page,
            Query = q
        };
        var result = await _ongHandler.GetAllAsync(request);

        return IsOperationValid() ? ResponseOk(result) : ResponseBadRequest();
    }

    [Authorize]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> Post(CreateONGRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ResponseBadRequest(ModelState);
        }
        await _ongHandler.CreateAsync(request);

        return IsOperationValid() ? ResponseCreated() : ResponseBadRequest();
    }

    [Authorize]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
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

    [Authorize]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _ongHandler.DeleteAsync(new DeleteONGRequest(id));

        return IsOperationValid() ? ResponseOk() : ResponseBadRequest();
    }
}
