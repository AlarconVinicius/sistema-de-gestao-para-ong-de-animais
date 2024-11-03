using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Requests.Shared;

namespace SGONGA.WebAPI.API.Users.Commands.Update;

public sealed record UpdateUserRequest(
        Guid Id,
        EUsuarioTipo UsuarioTipo,
        string Nome,
        string Apelido,
        string Documento,
        string Site,
        ContatoRequest Contato,
        string Senha,
        string ConfirmarSenha,
        bool TelefoneVisivel,
        bool AssinarNewsletter,
        DateTime DataNascimento,
        string Estado,
        string Cidade,
        string? Sobre,
        string? ChavePix);