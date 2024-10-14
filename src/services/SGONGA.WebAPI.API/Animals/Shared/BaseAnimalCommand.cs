using SGONGA.WebAPI.API.Abstractions.Messaging;

namespace SGONGA.WebAPI.API.Animals.Shared;

public abstract record BaseAnimalCommand(
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
    string ChavePix = "") : ICommand;