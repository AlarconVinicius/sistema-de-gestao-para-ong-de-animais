using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.Business.Errors;

public static class AnimalErrors
{
    public static Error AnimalNaoEncontrado(Guid id) => Error.NotFound("ANIMAL_NAO_ENCONTRADO", $"O animal com o Id = '{id}' não foi encontrado.");
    public static readonly Error NenhumAnimalEncontrado = Error.NotFound("NENHUM_ANIMAL_ENCONTRADO", "Nenhum animal encontrado.");
    
    public static readonly Error NaoFoiPossivelRecuperarAnimal = Error.Failure("RECUPERAR_ANIMAL_FALHA", "Não foi possível recuperar o animal.");
    public static readonly Error NaoFoiPossivelRecuperarAnimais = Error.Failure("RECUPERAR_ANIMALS_FALHA", "Não foi possível recuperar os animais.");
    public static readonly Error NaoFoiPossivelCriarAnimal = Error.Failure("CRIAR_ANIMAL_FALHA", "Não foi possível criar o animal.");
    public static readonly Error NaoFoiPossivelAtualizarAnimal = Error.Failure("ATUALIZAR_ANIMAL_FALHA", "Não foi possível atualizar o animal.");
    public static readonly Error NaoFoiPossivelDeletarAnimal = Error.Failure("DELETAR_ANIMAL_FALHA", "Não foi possível deletar o animal.");
}