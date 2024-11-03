using SGONGA.WebAPI.API.Users.Commands.Create;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Requests.Shared;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace SGONGA.WebAPI.API.Users.Examples;

[ExcludeFromCodeCoverage]
public class CreatePersonExample : IMultipleExamplesProvider<CreateUserCommand>
{
    IEnumerable<SwaggerExample<CreateUserCommand>> IMultipleExamplesProvider<CreateUserCommand>.GetExamples()
    {
        yield return SwaggerExample.Create(
            "Cadastrar um adotante",
            new CreateUserCommand(
            UsuarioTipo: EUsuarioTipo.Adotante,
            Nome: "João Silva Santos",
            Apelido: "João Silva",
            Documento: "12345678900",
            Site: "www.joaosilva.com.br",
            Contato: new ContatoRequest(
                telefone: "(11) 98765-4321",
                email: "joao.silva@email.com"
            ),
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
                UsuarioTipo: EUsuarioTipo.ONG,
                Nome: "Amigos dos Animais Proteção e Bem-estar",
                Apelido: "Amigos dos Animais",
                Documento: "12345678000199",
                Site: "www.amigosdosanimais.org.br",
                Contato: new ContatoRequest(
                    telefone: "(11) 3456-7890",
                    email: "contato@amigosdosanimais.org.br"
                ),
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