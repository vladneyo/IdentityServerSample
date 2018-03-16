using System;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServerSample.IdentityServer.EDM;
using IdentityServerSample.IdentityServer.EDM.Models;
using IdentityServerSample.Shared.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServerSample.IdentityServer.Host
{
    public class SeedData
    {
        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            EnsureSeedUsers(serviceProvider);
            EnsureSeedISSetup(serviceProvider);
            Console.ReadKey();
        }

        private static void EnsureSeedUsers(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                const string adminUsername = "identity_server_admin";

                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.Migrate();

                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var ISAdmin = userMgr.FindByNameAsync(adminUsername).Result;

                if(ISAdmin != null)
                {
                    Console.WriteLine($"{adminUsername} already exists");
                }

                if (ISAdmin == null)
                {
                    ISAdmin = new ApplicationUser
                    {
                        UserName = adminUsername
                    };
                    var result = userMgr.CreateAsync(ISAdmin, "Q1w2e3r4!!").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result = userMgr.AddClaimsAsync(ISAdmin, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "ISAdmin"),
                        new Claim(JwtClaimTypes.Email, "isadmin@mail.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", System.Security.Claims.ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Role, ISRoles.Admin),
                        new Claim(JwtClaimTypes.Scope, $"{StandardScopes.OpenId} {StandardScopes.Email} name {ISClients.ISAdminPanelClientId}")
                    }).Result;

                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    Console.WriteLine($"{adminUsername} created");
                }
            }
        }

        private static void EnsureSeedISSetup(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();

                var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();
                context.Database.Migrate();

                Console.WriteLine("Seeding database...");

                if (!context.Clients.Any())
                {
                    Console.WriteLine("Clients being populated");
                    foreach (var client in Config.GetClients().ToList())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Clients already populated");
                }

                if (!context.IdentityResources.Any())
                {
                    Console.WriteLine("IdentityResources being populated");
                    foreach (var resource in Config.GetIdentityResources().ToList())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("IdentityResources already populated");
                }

                if (!context.ApiResources.Any())
                {
                    Console.WriteLine("ApiResources being populated");
                    foreach (var resource in Config.GetApiResources().ToList())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("ApiResources already populated");
                }

                Console.WriteLine("Done seeding database.");
                Console.WriteLine();
            }
        }
    }
}
