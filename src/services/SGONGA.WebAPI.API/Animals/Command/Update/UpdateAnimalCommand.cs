namespace SGONGA.WebAPI.API.Animals.Command.Update;

public sealed record UpdateAnimalCommand : BaseAnimalCommand
{
    public UpdateAnimalCommand(
        Guid id,
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
        string ChavePix = "") : base(Nome, Especie, Raca, Sexo, Castrado, Cor, Porte, Idade, Descricao, Observacao, Foto, ChavePix)
    {
        Id = id;
    }
    public Guid Id { get; set; }
}
