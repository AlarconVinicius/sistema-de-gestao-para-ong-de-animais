using Bogus;
using SGONGA.WebAPI.API.Animals.Commands.Create;
using SGONGA.WebAPI.Business.Animals.Responses;
using SGONGA.WebAPI.Business.Models;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Printing;

namespace SGONGA.WebAPI.Mocks;


[ExcludeFromCodeCoverage]
public static class AnimalDataFaker
{
    public static Animal GenerateValidAnimal()
    {
        var faker = new Faker<Animal>("pt_BR")
            .CustomInstantiator(f =>
            {
                var name = f.Name.FirstName();
                return Animal.Create(
                        Guid.NewGuid(),
                        name,
                        f.PickRandom(new[] { "Cachorro", "Gato" }),
                        f.PickRandom(new[] { "SRD" }),
                        f.Random.Bool(),
                        f.Random.Bool(),
                        f.PickRandom(new[] { "Branco", "Preto", "Laranja", "Caramelo" }),
                        f.PickRandom(new[] { "Pequeno", "Médio", "Grande" }),
                        f.Random.Int(1, 15).ToString() + " anos",
                        f.Lorem.Sentence(),
                        f.Lorem.Sentence(),
                        f.Image.PicsumUrl(),
                        f.Internet.Email(name, "").ToLower()
                );
            });
        return faker.Generate();
    }
    public static List<Animal> GenerateValidDataList(int count = 5)
    {
        var faker = new Faker<Animal>("pt_BR")
            .CustomInstantiator(f =>
            {
                var name = f.Name.FirstName();
                return Animal.Create(
                        Guid.NewGuid(),
                        name,
                        f.PickRandom(new[] { "Cachorro", "Gato" }),
                        f.PickRandom(new[] { "SRD" }),
                        f.Random.Bool(),
                        f.Random.Bool(),
                        f.PickRandom(new[] { "Branco", "Preto", "Laranja", "Caramelo" }),
                        f.PickRandom(new[] { "Pequeno", "Médio", "Grande" }),
                        f.Random.Int(1, 15).ToString() + " anos",
                        f.Lorem.Sentence(),
                        f.Lorem.Sentence(),
                        f.Image.PicsumUrl(),
                        f.Internet.Email(name)
                );
            });

        return faker.Generate(count);
    }

    public static (Guid tenantId, string nome, string especie, string raca, bool sexo, bool castrado, string cor, string porte, string idade, string descricao, string observacao, string foto, string chavePix) GenerateInvalidAnimal()
    {
        return (
            Guid.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            new Faker().Random.Bool(),
            new Faker().Random.Bool(),
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty
        );
    }
    public static CreateAnimalCommand GenerateValidCreateAnimalCommand()
    {
        var faker = new Faker<CreateAnimalCommand>("pt_BR")
            .CustomInstantiator(f =>
            {
                var name = f.Name.FirstName();
                return new CreateAnimalCommand(
                        name,
                        f.PickRandom(new[] { "Cachorro", "Gato" }),
                        f.PickRandom(new[] { "SRD" }),
                        f.Random.Bool(),
                        f.Random.Bool(),
                        f.PickRandom(new[] { "Branco", "Preto", "Laranja", "Caramelo" }),
                        f.PickRandom(new[] { "Pequeno", "Médio", "Grande" }),
                        f.Random.Int(1, 15).ToString() + " anos",
                        f.Lorem.Sentence(),
                        f.Lorem.Sentence(),
                        f.Image.PicsumUrl(),
                        f.Internet.Email(name, "").ToLower()
                );
            });
        return faker.Generate();
    }

    public static CreateAnimalCommand GenerateInvalidCreateAnimalCommand()
    {
        return new CreateAnimalCommand(
            "",
            "",
            "",
            false,
            false,
            "",
            "",
            "",
            "",
            "",
            "",
            ""
        );
    }

    public static AnimalResponse GenerateValidAnimalResponse()
    {
        var faker = new Faker<AnimalResponse>("pt_BR")
            .CustomInstantiator(f =>
            {
                var name = f.Name.FirstName();
                return new AnimalResponse(
                        Guid.NewGuid(),
                        Guid.NewGuid(),
                        name,
                        f.PickRandom(new[] { "Cachorro", "Gato" }),
                        f.PickRandom(new[] { "SRD" }),
                        f.Random.Bool(),
                        f.Random.Bool(),
                        f.PickRandom(new[] { "Branco", "Preto", "Laranja", "Caramelo" }),
                        f.PickRandom(new[] { "Pequeno", "Médio", "Grande" }),
                        f.Random.Int(1, 15).ToString() + " anos",
                        f.Company.CompanyName(),
                        $"${f.Address.State()}, {f.Address.City()}",
                        f.Lorem.Sentence(),
                        f.Lorem.Sentence(),
                        f.Internet.Email(name, "").ToLower(),
                        f.Image.PicsumUrl(),
                        DateTime.UtcNow,
                        DateTime.UtcNow
                );
            });
        return faker.Generate();
    }
    public static BasePagedResponse<AnimalResponse> GenerateValidAnimalsResponse(int count)
    {
        var faker = new Faker<AnimalResponse>("pt_BR")
            .CustomInstantiator(f =>
            {
                var name = f.Name.FirstName();
                return new AnimalResponse(
                        Guid.NewGuid(),
                        Guid.NewGuid(),
                        name,
                        f.PickRandom(new[] { "Cachorro", "Gato" }),
                        f.PickRandom(new[] { "SRD" }),
                        f.Random.Bool(),
                        f.Random.Bool(),
                        f.PickRandom(new[] { "Branco", "Preto", "Laranja", "Caramelo" }),
                        f.PickRandom(new[] { "Pequeno", "Médio", "Grande" }),
                        f.Random.Int(1, 15).ToString() + " anos",
                        f.Company.CompanyName(),
                        $"${f.Address.State()}, {f.Address.City()}",
                        f.Lorem.Sentence(),
                        f.Lorem.Sentence(),
                        f.Internet.Email(name, "").ToLower(),
                        f.Image.PicsumUrl(),
                        DateTime.UtcNow,
                        DateTime.UtcNow
                );
            });

        List<AnimalResponse> list = faker.Generate(count);

        return new BasePagedResponse<AnimalResponse>()
        {
            List = list,
            TotalResults = list.Count,
            PageIndex = 1,
            PageSize = list.Count
        };
    }
}
