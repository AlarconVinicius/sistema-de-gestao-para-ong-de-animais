using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.Business.Errors;

public static class ONGErrors
{
    public static Error ONGNaoEncontrada(Guid id) => Error.NotFound("ONG_NAO_ENCONTRADO", $"A ONG com o Id = '{id}' não foi encontrada.");
    public static readonly Error NenhumaONGEncontrada = Error.NotFound("NENHUM_ONG_ENCONTRADO", "Nenhuma ONG encontrada.");

    public static readonly Error NaoFoiPossivelRecuperarONG = Error.Failure("RECUPERAR_ONG_FALHA", "Não foi possível recuperar a ONG.");
    public static readonly Error NaoFoiPossivelRecuperarONGs = Error.Failure("RECUPERAR_ONGS_FALHA", "Não foi possível recuperar as ONGs.");
    public static readonly Error NaoFoiPossivelCriarONG = Error.Failure("CRIAR_ONG_FALHA", "Não foi possível criar a ONG.");
    public static readonly Error NaoFoiPossivelAtualizarONG = Error.Failure("ATUALIZAR_ONG_FALHA", "Não foi possível atualizar a ONG.");
    public static readonly Error NaoFoiPossivelDeletarONG = Error.Failure("DELETAR_ONG_FALHA", "Não foi possível deletar a ONG.");
}
