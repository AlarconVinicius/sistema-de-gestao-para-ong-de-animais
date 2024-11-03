using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Models.DomainObjects;
using SGONGA.WebAPI.Data.Context;

namespace SGONGA.WebAPI.Data.Seed;

public static class SeedONGData
{
    public static void SeedUsuarioDefault(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ONGDbContext>();
            var userId = Guid.Parse("2f805cb2-1c01-4d88-92ec-a989bad5b0af");
            var tenantId = Guid.Parse("05bf1089-6015-4c2d-bf7c-1cbfa920ce23");

            if (!context.ONGs.IgnoreQueryFilters().Any(o => o.Id == userId))
            {
                var user = ONG.Create(
                    id: userId,
                    tenantId: tenantId,
                    nome: "Tenant Default",
                    apelido: "Tenant Default",
                    documento: "10136070078",
                    site: "https://www.instagram.com/Tenant-Default/?hl=pt-br",
                    contato: new Contato("(24) 96978-4572", "tenant@email.com"),
                    telefoneVisivel: true,
                    assinarNewsletter: true,
                    dataNascimento: new DateTime(1990, 1, 1),
                    estado: "Rio de Janeiro",
                    cidade: "Rio de Janeiro",
                    sobre: "Usuário Default da aplicação.",
                    chavePix: "10136070078"
                );

                context.ONGs.Add(user);
                context.SaveChanges();
            }
        }
    }
}