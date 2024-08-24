using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Models.DomainObjects;
using SGONGA.WebAPI.Data.Context;

namespace SGONGA.WebAPI.Data.Seed;

public static class SeedONGData
{
    public static void SeedONGAndColaborador(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ONGDbContext>();
            var ongId = Guid.Parse("05bf1089-6015-4c2d-bf7c-1cbfa920ce23");
            var userId = Guid.Parse("2f805cb2-1c01-4d88-92ec-a989bad5b0af");

            if ((!context.ONGs.IgnoreQueryFilters().Any(o => o.Id == ongId)) && (!context.Colaboradores.IgnoreQueryFilters().Any(c => c.Id == userId)))
            {
                var ong = new ONG(
                    id: ongId,
                    nome: "Tenant Default",
                    instagram: "https://www.instagram.com/Tenant-Default/?hl=pt-br",
                    documento: "10136070078",
                    contato: new Contato("(24) 96978-4572", "tenant@email.com"),
                    endereco: new Endereco("Rio de Janeiro", "RJ", "60341-370", "Vila Socorro de Farias", "Jardim Iracema", 934, null, null),
                    chavePix: "tenant@email.com"
                );

                context.ONGs.Add(ong);
            
                var colaborador = new Colaborador(
                    id: userId,
                    tenantId: ongId,
                    nome: "Colaborador Default",
                    documento: "10136070078",
                    contato: new Contato("(24) 96978-4572", "tenant@email.com"),
                    dataNascimento: new DateTime(1990, 1, 1)
                );

                context.Colaboradores.Add(colaborador);
                context.SaveChanges();
            }
        }
    }
}