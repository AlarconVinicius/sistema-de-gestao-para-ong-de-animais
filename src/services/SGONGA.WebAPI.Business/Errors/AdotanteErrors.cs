using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.Business.Errors;

public static class AdotanteErrors
{
    public static Error AdotanteNaoEncontrado(Guid id) => Error.NotFound("ADOTANTE_NAO_ENCONTRADO", $"O usuário com o Id = '{id}' não foi encontrado.");
    public static readonly Error NenhumAdotanteEncontrado = Error.NotFound("NENHUM_ADOTANTE_ENCONTRADO", "Nenhum usuário encontrado.");

    public static readonly Error NaoFoiPossivelRecuperarAdotante = Error.Failure("RECUPERAR_ADOTANTE_FALHA", "Não foi possível recuperar o usuário.");
    public static readonly Error NaoFoiPossivelRecuperarAdotantes = Error.Failure("RECUPERAR_ADOTANTES_FALHA", "Não foi possível recuperar os usuários.");
    public static readonly Error NaoFoiPossivelCriarAdotante = Error.Failure("CRIAR_ADOTANTE_FALHA", "Não foi possível criar o usuário.");
    public static readonly Error NaoFoiPossivelAtualizarAdotante = Error.Failure("ATUALIZAR_ADOTANTE_FALHA", "Não foi possível atualizar o usuário.");
    public static readonly Error NaoFoiPossivelDeletarAdotante = Error.Failure("DELETAR_ADOTANTE_FALHA", "Não foi possível deletar o usuário.");
}
public enum ErrorType2
{
    Failure = 0,
    Validation = 1,
    NotFound = 2,
    Conflict = 3,
    Forbidden = 4
}