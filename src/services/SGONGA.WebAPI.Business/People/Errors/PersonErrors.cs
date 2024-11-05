using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.Business.People.Errors;

public static class PersonErrors
{
    public static Error UsuarioNaoEncontrado(Guid id) => Error.NotFound("USUARIO_NAO_ENCONTRADO", $"O usuário com o Id = '{id}' não foi encontrado.");
    public static readonly Error NenhumUsuarioEncontrado = Error.NotFound("NENHUM_USUARIO_ENCONTRADO", "Nenhum usuário encontrado.");

    public static Error EmailEmUso(string email) => Error.Conflict("EMAIL_EM_USO", $"O email = '{email}' já está em uso.");
    public static Error ApelidoEmUso(string apelido) => Error.Conflict("APELIDO_EM_USO", $"O apelido = '{apelido}' já está em uso.");
    public static Error DocumentoEmUso(string documento) => Error.Conflict("DOCUMENTO_EM_USO", $"O documento = '{documento}' já está em uso.");

    public static readonly Error NaoFoiPossivelRecuperarUsuario = Error.Failure("RECUPERAR_USUARIO_FALHA", "Não foi possível recuperar o usuário.");
    public static readonly Error NaoFoiPossivelRecuperarUsuarios = Error.Failure("RECUPERAR_USUARIOS_FALHA", "Não foi possível recuperar os usuários.");
    public static readonly Error NaoFoiPossivelCriarUsuario = Error.Failure("CRIAR_USUARIO_FALHA", "Não foi possível criar o usuário.");
    public static readonly Error NaoFoiPossivelAtualizarUsuario = Error.Failure("ATUALIZAR_USUARIO_FALHA", "Não foi possível atualizar o usuário.");
    public static readonly Error NaoFoiPossivelDeletarUsuario = Error.Failure("DELETAR_USUARIO_FALHA", "Não foi possível deletar o usuário.");
}
