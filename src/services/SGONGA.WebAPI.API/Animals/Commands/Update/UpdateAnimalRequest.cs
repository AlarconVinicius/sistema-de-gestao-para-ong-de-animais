namespace SGONGA.WebAPI.API.Animals.Commands.Update;

public sealed record UpdateAnimalRequest(
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
    string ChavePix = "");