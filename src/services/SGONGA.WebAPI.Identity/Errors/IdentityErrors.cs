using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.Identity.Errors;
public class IdentityErrors
{
    public static Error UsuarioNaoEncontrado(Guid id) => Error.NotFound("", $"O usuário com o Id = '{id}' não foi encontrado.");
    public static Error UsuarioNaoEncontrado(string email) => Error.Conflict("", $"O usuário com o email = '{email}' não foi encontrado.");
    public static readonly Error NenhumAdotanteEncontrado = Error.NotFound("", "Nenhum usuário encontrado.");

    public static readonly Error UsuarioTemporariamenteBloqueado = Error.Validation("", "Usuário temporariamente bloqueado devido às tentativas inválidas.");
    public static readonly Error Credenciais_Invalidas = Error.Validation("", "Usuário ou senha inválidos.");
    public static readonly Error SenhasNaoConferem = Error.Validation("", "Usuário ou senha inválidos.");
    public static readonly Error SenhaAtualInvalida = Error.Validation("", "A senha atual está incorreta.");

    public static readonly Error NaoFoiPossivelDeletarAdotante = Error.Conflict("", "Não foi possível realizar a exclusão do usuário.");

    public static readonly Error NaoFoiPossivelRecuperarAdotante = Error.Failure("", "Não foi possível recuperar o usuário.");
    public static readonly Error NaoFoiPossivelRecuperarAdotantes = Error.Failure("", "Não foi possível recuperar os usuários.");
    public static readonly Error NaoFoiPossivelCriarAdotante = Error.Failure("", "Não foi possível criar o usuário.");
    public static readonly Error NaoFoiPossivelAtualizarAdotante = Error.Failure("", "Não foi possível atualizar o usuário.");
}