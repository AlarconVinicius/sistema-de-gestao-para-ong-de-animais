using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.Business.Errors;

public static class UsuarioErrors
{
    public static Error UsuarioNaoEncontrado(Guid id) => Error.NotFound("USUARIO_NAO_ENCONTRADO", $"O usuário com o Id = '{id}' não foi encontrado.");
    public static readonly Error NenhumUsuarioEncontrado = Error.NotFound("NENHUM_USUARIO_ENCONTRADO", "Nenhum usuário encontrado.");

    public static readonly Error NaoFoiPossivelRecuperarUsuario = Error.Failure("RECUPERAR_USUARIO_FALHA", "Não foi possível recuperar o usuário.");
    public static readonly Error NaoFoiPossivelRecuperarUsuarios = Error.Failure("RECUPERAR_USUARIOS_FALHA", "Não foi possível recuperar os usuários.");
    public static readonly Error NaoFoiPossivelCriarUsuario = Error.Failure("CRIAR_USUARIO_FALHA", "Não foi possível criar o usuário.");
    public static readonly Error NaoFoiPossivelAtualizarUsuario = Error.Failure("ATUALIZAR_USUARIO_FALHA", "Não foi possível atualizar o usuário.");
    public static readonly Error NaoFoiPossivelDeletarUsuario = Error.Failure("DELETAR_USUARIO_FALHA", "Não foi possível deletar o usuário.");
}
