using SGONGA.WebAPI.API.Animals.Command.CreateAnimal;
using SGONGA.WebAPI.Business.Models;

namespace SGONGA.WebAPI.API.Animals.Shared;

public static class AnimalMappings
{
    public static Animal MapCommandToDomain(this CreateAnimalCommand command)
    {
        if (command == null)
        {
            return null!;
        }

        return new Animal(command.Nome, command.Especie, command.Raca, command.Sexo, command.Castrado, command.Cor, command.Porte, command.Idade, command.Descricao, command.Observacao, command.Foto, command.ChavePix);
    }
}
