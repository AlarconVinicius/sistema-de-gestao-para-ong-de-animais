using Bogus;
using SGONGA.WebAPI.Business.Models;

namespace SGONGA.Tests.Animals.Shared;

public static class AnimalDataFaker
{
    public static Animal GenerateValidData()
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

    public static (Guid tenantId, string nome, string especie, string raca, bool sexo, bool castrado, string cor, string porte, string idade, string descricao, string observacao, string foto, string chavePix) GenerateInvalidData()
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
}
