﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGONGA.Core.Configurations;
using SGONGA.Core.Notifications;
using SGONGA.WebAPI.API.Controllers.Shared;
using SGONGA.WebAPI.Business.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Requests;

namespace SGONGA.WebAPI.API.Controllers;

[Authorize]
[Route("api/v1/usuarios/admin/")]
public class UsuariosController : ApiController
{
    public readonly IUsuarioHandler _usuarioHandler;
    public UsuariosController(INotifier notifier, IUsuarioHandler usuarioHandler) : base(notifier)
    {
        _usuarioHandler = usuarioHandler;
    }

    #region Public Methods

    /// <summary>
    /// Cadastro de usuario.
    /// </summary>
    /// <remarks>
    /// Este endpoint permite o cadastro de um novo usuario. 
    /// Não é necessário fornecer cabeçalhos de autenticação ou identificação de locatário para este endpoint.
    /// </remarks>
    /// <param name="request">Dados de cadastro do usuario</param>
    /// <returns>Resposta indicando o sucesso ou falha na criação do usuario</returns>
    /// <response code="201">Usuario criado com sucesso</response>
    /// <response code="400">Retorna erros de validação</response>
    [AllowAnonymous]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [HttpPost("/api/v1/usuarios")]
    public async Task<IActionResult> Post(CreateUsuarioRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ResponseBadRequest(ModelState);
        }
        await _usuarioHandler.CreateAsync(request);

        return IsOperationValid() ? ResponseCreated() : ResponseBadRequest();
    }

    /// <summary>
    /// Retorna as informações de um usuario público pelo ID.
    /// </summary>
    /// <remarks>
    /// Este endpoint permite recuperar as informações de um usuario específico de forma pública.
    /// Não é necessário fornecer cabeçalhos de autenticação ou identificação de locatário para este endpoint.
    /// </remarks>
    /// <param name="id">ID do usuario (GUID)</param>
    /// <returns>Detalhes do usuario</returns>
    /// <response code="200">Detalhes do usuario retornados com sucesso</response>
    /// <response code="400">Retorna erros relacionados à requisição</response>
    [AllowAnonymous]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [HttpGet("/api/v1/usuarios/{id:guid}")]
    public async Task<IActionResult> GetByIdPublic(Guid id)
    {
        GetUsuarioByIdRequest request = new(id, false);
        var result = await _usuarioHandler.GetByIdAsync(request);

        return IsOperationValid() ? ResponseOk(result) : ResponseBadRequest();

    }

    /// <summary>
    /// Retorna todos os usuarios públicos.
    /// </summary>
    /// <remarks>
    /// Este endpoint permite recuperar uma lista de todos os usuarios de forma pública.
    /// Não é necessário fornecer cabeçalhos de autenticação ou identificação de locatário para este endpoint.
    /// </remarks>
    /// <param name="ps">Tamanho da página para paginação (padrão: 25)</param>
    /// <param name="page">Número da página para paginação (padrão: 1)</param>
    /// <param name="q">Consulta para filtragem de usuarios</param>
    /// <param name="tipo">Tipo de usuário a ser buscado, 0 para Adotante e 1 para ONG</param>
    /// <returns>Lista de usuarios</returns>
    /// <response code="200">Lista de usuarios retornada com sucesso</response>
    /// <response code="400">Retorna erros relacionados à requisição</response>
    [AllowAnonymous]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [HttpGet("/api/v1/usuarios")]
    public async Task<IActionResult> GetAllPublic([FromQuery] int ps = ConfigurationDefault.DefaultPageSize, [FromQuery] int page = ConfigurationDefault.DefaultPageNumber, [FromQuery] string q = null!, [FromQuery] int tipo = 0)
    {
        GetAllUsuariosRequest request = new(
            pageSize: ps,
            pageNumber: page,
            query: q,
            tenantFiltro: false,
            usuarioTipo: tipo
        );
        var result = await _usuarioHandler.GetAllAsync(request);

        return IsOperationValid() ? ResponseOk(result) : ResponseBadRequest();
    }
    #endregion

    #region Admin Methods

    /// <summary>
    /// Retorna as informações de um usuario pelo ID (Admin).
    /// </summary>
    /// <remarks>
    /// Este endpoint permite recuperar as informações de um usuario específico de forma administrativa.
    /// Para uso administrativo, é necessário fornecer os seguintes cabeçalhos na requisição:
    /// 
    /// - **Authorization**: Bearer Token para autenticação do usuário.
    /// - **TenantId**: ID do locatário para identificar o ambiente.
    /// 
    /// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    /// O `TenantId` deve ser o identificador do locatário que está fazendo a requisição.
    /// </remarks>
    /// <param name="id">ID do usuario (GUID)</param>
    /// <returns>Detalhes do usuario</returns>
    /// <response code="200">Detalhes do usuario retornados com sucesso</response>
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
        GetUsuarioByIdRequest request = new(id, true);
        var result = await _usuarioHandler.GetByIdAsync(request);

        return IsOperationValid() ? ResponseOk(result) : ResponseBadRequest();

    }

    /// <summary>
    /// Retorna todos os usuarios (Admin).
    /// </summary>
    /// <remarks>
    /// Este endpoint permite recuperar uma lista de todos os usuarios de forma administrativa.
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
    /// <param name="q">Consulta para filtragem de usuarios</param>
    /// <param name="tipo">Tipo de usuário a ser buscado, 0 para Adotante e 1 para ONG</param>
    /// <returns>Lista de usuarios</returns>
    /// <response code="200">Lista de usuarios retornada com sucesso</response>
    /// <response code="400">Retorna erros relacionados à requisição</response>
    /// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    /// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int ps = ConfigurationDefault.DefaultPageSize, [FromQuery] int page = ConfigurationDefault.DefaultPageNumber, [FromQuery] string q = null!, [FromQuery] int tipo = 0)
    {
        GetAllUsuariosRequest request = new(
            pageSize: ps,
            pageNumber: page,
            query: q,
            tenantFiltro: true,
            usuarioTipo: tipo
        );
        var result = await _usuarioHandler.GetAllAsync(request);

        return IsOperationValid() ? ResponseOk(result) : ResponseBadRequest();
    }

    /// <summary>
    /// Atualização de usuario (Admin).
    /// </summary>
    /// <remarks>
    /// Este endpoint permite a atualização dos dados de um usuario. 
    /// Para uso administrativo, é necessário fornecer os seguintes cabeçalhos na requisição:
    /// 
    /// - **Authorization**: Bearer Token para autenticação do usuário.
    /// - **TenantId**: ID do locatário para identificar o ambiente.
    /// 
    /// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    /// O `TenantId` deve ser o identificador do locatário que está fazendo a requisição.
    /// </remarks>
    /// <param name="id">ID do usuario (GUID)</param>
    /// <param name="request">Dados atualizados do usuario</param>
    /// <returns>Resposta indicando o sucesso ou falha na atualização do usuario</returns>
    /// <response code="200">Usuario atualizado com sucesso</response>
    /// <response code="400">Retorna erros de validação ou IDs não correspondentes</response>
    /// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    /// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateUsuarioRequest request)
    {
        if (id != request.Id) return ResponseBadRequest("Os IDs não correspondem.");
        if (!ModelState.IsValid)
        {
            return ResponseBadRequest(ModelState);
        }

        await _usuarioHandler.UpdateAsync(request);

        return IsOperationValid() ? ResponseOk() : ResponseBadRequest();
    }

    /// <summary>
    /// Deleta um usuario (Admin).
    /// </summary>
    /// <remarks>
    /// Este endpoint permite a exclusão de um usuario específico. 
    /// Para uso administrativo, é necessário fornecer os seguintes cabeçalhos na requisição:
    /// 
    /// - **Authorization**: Bearer Token para autenticação do usuário.
    /// - **TenantId**: ID do locatário para identificar o ambiente.
    /// 
    /// O token deve ser obtido no fluxo de autenticação e incluído no formato `Bearer {token}`.
    /// O `TenantId` deve ser o identificador do locatário que está fazendo a requisição.
    /// </remarks>
    /// <param name="id">ID do usuario (GUID)</param>
    /// <returns>Resposta indicando o sucesso ou falha na exclusão do usuario</returns>
    /// <response code="200">Usuario deletado com sucesso</response>
    /// <response code="400">Retorna erros relacionados à requisição</response>
    /// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    /// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _usuarioHandler.DeleteAsync(new DeleteUsuarioRequest(id));

        return IsOperationValid() ? ResponseOk() : ResponseBadRequest();
    }
    #endregion
}