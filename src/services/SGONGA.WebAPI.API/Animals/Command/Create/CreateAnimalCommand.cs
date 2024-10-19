namespace SGONGA.WebAPI.API.Animals.Command.Create;

public sealed record CreateAnimalCommand(
    string Nome,
    string Especie,
    string Raca,
    bool Sexo,
    bool Castrado,
    string Cor,
    string Porte,
    string Idade,
    string Descricao,
    string Observacao,
    string Foto,
    string ChavePix = "") : BaseAnimalCommand(Nome, Especie, Raca, Sexo, Castrado, Cor, Porte, Idade, Descricao, Observacao, Foto, ChavePix);
