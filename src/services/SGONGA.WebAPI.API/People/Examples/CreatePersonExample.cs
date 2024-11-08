using SGONGA.WebAPI.API.People.Commands.Create;
using SGONGA.WebAPI.Business.People.Enum;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace SGONGA.WebAPI.API.People.Examples;

[ExcludeFromCodeCoverage]
public class CreatePersonExample : IMultipleExamplesProvider<CreatePersonCommand>
{
    IEnumerable<SwaggerExample<CreatePersonCommand>> IMultipleExamplesProvider<CreatePersonCommand>.GetExamples()
    {
        yield return SwaggerExample.Create(
            "Cadastrar um adotante",
            new CreatePersonCommand(
            PersonType: EPersonType.Adopter,
            Name: "João Silva Santos",
            Nickname: "João Silva",
            Document: "12345678900",
            Site: "www.joaosilva.com.br",
            PhoneNumber: "11987654321",
            Email: "joao.silva@email.com",
            Password: "Senha@123456",
            ConfirmPassword: "Senha@123456",
            IsPhoneNumberVisible: true,
            SubscribeToNewsletter: true,
            BirthDate: new DateTime(1990, 5, 15),
            State: "SP",
            City: "São Paulo",
            About: "Profissional com mais de 10 anos de experiência em desenvolvimento de software.",
            PixKey: null
        ));

        yield return SwaggerExample.Create(
            "Cadastrar uma ONG",
            new CreatePersonCommand(
                PersonType: EPersonType.NGO,
                Name: "Amigos dos Animais Proteção e Bem-estar",
                Nickname: "Amigos dos Animais",
                Document: "12345678000199",
                Site: "www.amigosdosanimais.org.br",
                PhoneNumber: "11987654321",
                Email: "joao.silva@email.com",
                Password: "Senha@123456",
                ConfirmPassword: "Senha@123456",
                IsPhoneNumberVisible: true,
                SubscribeToNewsletter: true,
                BirthDate: new DateTime(2010, 3, 20),
                State: "SP",
                City: "São Paulo",
                About: "ONG dedicada ao resgate, reabilitação e adoção responsável de cães e gatos. " +
                      "Atuamos desde 2010 na região metropolitana de São Paulo, " +
                      "promovendo feiras de adoção e campanhas de conscientização.",
                PixKey: "12345678000199"
        ));
    }
}