using SGONGA.WebAPI.API.People.Commands.Create;
using SGONGA.WebAPI.Business.People.Enum;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace SGONGA.WebAPI.API.People.Examples;

[ExcludeFromCodeCoverage]
public class CreatePersonExample : IMultipleExamplesProvider<CreateUserCommand>
{
    IEnumerable<SwaggerExample<CreateUserCommand>> IMultipleExamplesProvider<CreateUserCommand>.GetExamples()
    {
        yield return SwaggerExample.Create(
            "Cadastrar um adotante",
            new CreateUserCommand(
            UsuarioTipo: EPersonType.Adopter,
            Nome: "João Silva Santos",
            Apelido: "João Silva",
            Documento: "12345678900",
            Site: "www.joaosilva.com.br",
            Telefone: "11987654321",
            Email: "joao.silva@email.com",
            Senha: "Senha@123456",
            ConfirmarSenha: "Senha@123456",
            TelefoneVisivel: true,
            AssinarNewsletter: true,
            DataNascimento: new DateTime(1990, 5, 15),
            Estado: "SP",
            Cidade: "São Paulo",
            Sobre: "Profissional com mais de 10 anos de experiência em desenvolvimento de software.",
            ChavePix: null
        ));

        yield return SwaggerExample.Create(
            "Cadastrar uma ONG",
            new CreateUserCommand(
                UsuarioTipo: EPersonType.NGO,
                Nome: "Amigos dos Animais Proteção e Bem-estar",
                Apelido: "Amigos dos Animais",
                Documento: "12345678000199",
                Site: "www.amigosdosanimais.org.br",
                Telefone: "11987654321",
                Email: "joao.silva@email.com",
                Senha: "Senha@123456",
                ConfirmarSenha: "Senha@123456",
                TelefoneVisivel: true,
                AssinarNewsletter: true,
                DataNascimento: new DateTime(2010, 3, 20),
                Estado: "SP",
                Cidade: "São Paulo",
                Sobre: "ONG dedicada ao resgate, reabilitação e adoção responsável de cães e gatos. " +
                      "Atuamos desde 2010 na região metropolitana de São Paulo, " +
                      "promovendo feiras de adoção e campanhas de conscientização.",
                ChavePix: "12345678000199"
        ));
    }
}