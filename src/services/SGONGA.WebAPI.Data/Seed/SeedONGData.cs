﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SGONGA.WebAPI.Business.People.Entities;
using SGONGA.WebAPI.Data.Context;

namespace SGONGA.WebAPI.Data.Seed;

public static class SeedONGData
{
    public static void SeedUsuarioDefault(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<OrganizationDbContext>();
            var personId = Guid.Parse("2f805cb2-1c01-4d88-92ec-a989bad5b0af");
            var tenantId = Guid.Parse("05bf1089-6015-4c2d-bf7c-1cbfa920ce23");

            if (!context.Organizations.IgnoreQueryFilters().Any(o => o.Id == personId))
            {
                var person = Organization.Create(
                    id: personId,
                    tenantId: tenantId,
                    name: "Tenant Default",
                    nickname: "Tenant Default",
                    document: "10136070078",
                    site: "https://www.instagram.com/Tenant-Default/?hl=pt-br",
                    email: "tenant@email.com",
                    phoneNumber: "24969784572",
                    isPhoneNumberVisible: true,
                    subscribeToNewsletter: true,
                    birthDate: new DateTime(1990, 1, 1),
                    state: "RJ",
                    city: "Rio de Janeiro",
                    about: "Usuário Default da aplicação.",
                    pixKey: "10136070078"
                );

                context.Organizations.Add(person);
                context.SaveChanges();
            }
        }
    }
}