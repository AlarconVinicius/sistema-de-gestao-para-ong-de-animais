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
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        GetColaboradorByIdRequest request = new(id);
        var result = await _colaboradorHandler.GetByIdAsync(request);

        return IsOperationValid() ? ResponseOk(result) : ResponseBadRequest();

    }

    //[ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
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

    //[ClaimsAuthorize("Permissions", "SuperAdmin")]
    //[ProducesResponseType(typeof(CustomResult), StatusCodes.Status201Created)]
    //[ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
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

    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
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

    //[ClaimsAuthorize("Permissions", "SuperAdmin")]
    //[ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    //[HttpDelete("{id:guid}")]
    //public async Task<IActionResult> Delete(Guid id)
    //{
    //    await _colaboradorHandler.DeleteAsync(new DeleteColaboradorRequest(id));

    //    return IsOperationValid() ? ResponseOk() : ResponseBadRequest();
    //}
#endregion
}