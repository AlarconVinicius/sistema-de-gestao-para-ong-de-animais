using SGONGA.WebAPI.Business.People.Enum;

namespace SGONGA.WebAPI.API.People.Commands.Update;

public sealed record UpdateUserRequest(
        Guid Id,
        EPersonType UsuarioTipo,
        string Nome,
        string Apelido,
        string Documento,
        string Site,
        string Email,
        string Telefone,
        string Senha,
        string ConfirmarSenha,
        bool TelefoneVisivel,
        bool AssinarNewsletter,
        DateTime DataNascimento,
        string Estado,
        string Cidade,
        string? Sobre,
        string? ChavePix);