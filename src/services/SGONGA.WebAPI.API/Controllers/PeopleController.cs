using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGONGA.Core.Configurations;
using SGONGA.Core.Notifications;
using SGONGA.WebAPI.API.Controllers.Shared;
using SGONGA.WebAPI.API.Extensions;
using SGONGA.WebAPI.API.Users.Commands.Create;
using SGONGA.WebAPI.API.Users.Commands.Delete;
using SGONGA.WebAPI.API.Users.Commands.Update;
using SGONGA.WebAPI.API.Users.Queries.GetAll;
using SGONGA.WebAPI.API.Users.Queries.GetById;
using SGONGA.WebAPI.Business.Abstractions;
using SGONGA.WebAPI.Business.Animals.Responses;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Users.Responses;

namespace SGONGA.WebAPI.API.Controllers;

[Authorize]
[Route("api/v1/usuarios/admin/")]
public class PeopleController(INotifier notifier, ISender sender) : ApiController(notifier, sender)
{
    #region Public Methods

    /// <summary>
    /// Cadastro de usuario.
    /// </summary>
    /// <remarks>
    /// Este endpoint permite o cadastro de um novo usuario. 
    /// Não é necessário fornecer cabeçalhos de autenticação ou identificação de locatário para este endpoint.
    /// </remarks>
    /// <param name="command">Dados de cadastro do usuario</param>
    /// <param name="cancellationToken">Ignored</param>
    /// <returns>Resposta indicando o sucesso ou falha na criação do usuario</returns>
    /// <response code="201">Usuario criado com sucesso</response>
    /// <response code="400">Retorna erros de validação</response>
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("/api/v1/usuarios")]
    public async Task<IResult> Post(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.Match(
            onSuccess: () => Results.Created(),
            onFailure: response => response.ToProblemDetails());
    }

    /// <summary>
    /// Retorna as informações de um usuario público pelo ID.
    /// </summary>
    /// <remarks>
    /// Este endpoint permite recuperar as informações de um usuario específico de forma pública.
    /// Não é necessário fornecer cabeçalhos de autenticação ou identificação de locatário para este endpoint.
    /// </remarks>
    /// <param name="id">ID do usuario (GUID)</param>
    /// <param name="cancellationToken">Ignored</param>
    /// <returns>Detalhes do usuario</returns>
    /// <response code="200">Detalhes do usuario retornados com sucesso</response>
    /// <response code="400">Retorna erros relacionados à requisição</response>
    [AllowAnonymous]
    [ProducesResponseType(typeof(PersonResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("/api/v1/usuarios/{id:guid}")]
    public async Task<IResult> GetByIdPublic(Guid id, CancellationToken cancellationToken = default)
    {
        GetUserByIdQuery query = new(id, false);

        var result = await Sender.Send(query, cancellationToken);

        return result.Match(
            onSuccess: response => Results.Ok(response),
            onFailure: response => response.ToProblemDetails());
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
    /// <param name="cancellationToken">Ignored</param>
    /// <returns>Lista de usuarios</returns>
    /// <response code="200">Lista de usuarios retornada com sucesso</response>
    /// <response code="400">Retorna erros relacionados à requisição</response>
    [AllowAnonymous]
    [ProducesResponseType(typeof(BasePagedResponse<PersonResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("/api/v1/usuarios/adotantes")]
    public async Task<IResult> GetAllAdoptersPublic([FromQuery] int ps = ConfigurationDefault.DefaultPageSize, [FromQuery] int page = ConfigurationDefault.DefaultPageNumber, [FromQuery] string q = null!, [FromQuery] int tipo = 0, CancellationToken cancellationToken = default)
    {
        GetAllUsersQuery query = new(EUsuarioTipo.Adotante, ps, page, q, false, false);

        var result = await Sender.Send(query, cancellationToken);

        return result.Match(
            onSuccess: response => Results.Ok(response),
            onFailure: response => response.ToProblemDetails());
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
    /// <param name="cancellationToken">Ignored</param>
    /// <returns>Lista de usuarios</returns>
    /// <response code="200">Lista de usuarios retornada com sucesso</response>
    /// <response code="400">Retorna erros relacionados à requisição</response>
    [AllowAnonymous]
    [ProducesResponseType(typeof(BasePagedResponse<PersonResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("/api/v1/usuarios/ongs")]
    public async Task<IResult> GetAllNGOsPublic([FromQuery] int ps = ConfigurationDefault.DefaultPageSize, [FromQuery] int page = ConfigurationDefault.DefaultPageNumber, [FromQuery] string q = null!, [FromQuery] int tipo = 0, CancellationToken cancellationToken = default)
    {
        GetAllUsersQuery query = new(EUsuarioTipo.ONG, ps, page, q, false, false);

        var result = await Sender.Send(query, cancellationToken);

        return result.Match(
            onSuccess: response => Results.Ok(response),
            onFailure: response => response.ToProblemDetails());
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
    /// <param name="cancellationToken">Ignored</param>
    /// <returns>Detalhes do usuario</returns>
    /// <response code="200">Detalhes do usuario retornados com sucesso</response>
    /// <response code="400">Retorna erros relacionados à requisição</response>
    /// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    /// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    [ProducesResponseType(typeof(PersonResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpGet("{id:guid}")]
    public async Task<IResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        GetUserByIdQuery query = new(id, true);

        var result = await Sender.Send(query, cancellationToken);

        return result.Match(
            onSuccess: response => Results.Ok(response),
            onFailure: response => response.ToProblemDetails());
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
    /// <param name="cancellationToken">Ignored</param>
    /// <returns>Lista de usuarios</returns>
    /// <response code="200">Lista de usuarios retornada com sucesso</response>
    /// <response code="400">Retorna erros relacionados à requisição</response>
    /// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    /// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    [ProducesResponseType(typeof(BasePagedResponse<PersonResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpGet]
    public async Task<IResult> GetAllNGO([FromQuery] int ps = ConfigurationDefault.DefaultPageSize, [FromQuery] int page = ConfigurationDefault.DefaultPageNumber, [FromQuery] string q = null!, [FromQuery] int tipo = 0, CancellationToken cancellationToken = default)
    {
        GetAllUsersQuery query = new(EUsuarioTipo.ONG, ps, page, q, false, true);

        var result = await Sender.Send(query, cancellationToken);

        return result.Match(
            onSuccess: response => Results.Ok(response),
            onFailure: response => response.ToProblemDetails());
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
    /// <param name="cancellationToken">Ignored</param>
    /// <returns>Resposta indicando o sucesso ou falha na atualização do usuario</returns>
    /// <response code="204">Usuario atualizado com sucesso</response>
    /// <response code="400">Retorna erros de validação ou IDs não correspondentes</response>
    /// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    /// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPut("{id:guid}")]
    public async Task<IResult> Update(Guid id, UpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        UpdateUserCommand command = new(id, request.UsuarioTipo, request.Nome, request.Apelido, request.Documento, request.Site, new(request.Contato.Telefone, request.Contato.Email), request.TelefoneVisivel, request.AssinarNewsletter, request.DataNascimento, request.Estado, request.Estado, request.Sobre, request.ChavePix);

        var result = await Sender.Send(command, cancellationToken);

        return result.Match(
            onSuccess: () => Results.NoContent(),
            onFailure: response => response.ToProblemDetails());
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
    /// <param name="cancellationToken">Ignored</param>
    /// <returns>Resposta indicando o sucesso ou falha na exclusão do usuario</returns>
    /// <response code="204">Usuario deletado com sucesso</response>
    /// <response code="400">Retorna erros relacionados à requisição</response>
    /// <response code="401">Usuário não autorizado. Token de autenticação ausente ou inválido</response>
    /// <response code="403">Permissão negada. Usuário não possui privilégios para acessar este recurso</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpDelete("{id:guid}")]
    public async Task<IResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        DeleteUserCommand command = new(id);

        var result = await Sender.Send(command, cancellationToken);

        return result.Match(
            onSuccess: () => Results.NoContent(),
            onFailure: response => response.ToProblemDetails());
    }
    #endregion
}