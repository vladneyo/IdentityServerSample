using System;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
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
        const string ISAdminUsername = "identity_server_admin";
        const string BookingAdminUsername = "booking_admin";
        static readonly string ISAdminSubject = Guid.NewGuid().ToString();
        static readonly string BookingAdminSubject = Guid.NewGuid().ToString();
        const string ISAdminPassword = "Q1w2e3r4!!";
        const string BookingAdminPassword = "Q1w2e3r4!!";

        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            EnsureSeedUsers(serviceProvider);
            EnsureSeedISSetup(serviceProvider);
            Console.ReadKey();
        }

        private static void EnsureSeedUsers(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            using (var context = scope.ServiceProvider.GetService<ApplicationDbContext>())
            {
                context.Database.Migrate();

                using (var trx = context.Database.BeginTransaction())
                {
                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    #region ISAdmin

                    var ISAdmin = userMgr.FindByNameAsync(ISAdminUsername).Result;

                    if (ISAdmin != null)
                    {
                        Console.WriteLine($"{ISAdminUsername} already exists");
                    }

                    if (ISAdmin == null)
                    {
                        ISAdmin = new ApplicationUser
                        {
                            UserName = ISAdminUsername
                        };
                        var result = userMgr.CreateAsync(ISAdmin, ISAdminPassword).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        if (!roleMgr.RoleExistsAsync(ISRoles.Admin).Result)
                        {
                            result = roleMgr.CreateAsync(new IdentityRole(ISRoles.Admin)).Result;
                            if (!result.Succeeded)
                            {
                                throw new Exception(result.Errors.First().Description);
                            }
                        }

                        result = userMgr.AddToRoleAsync(ISAdmin, ISRoles.Admin).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(ISAdmin, new Claim[]{
                            new Claim(JwtClaimTypes.Subject, ISAdminSubject),
                            new Claim(JwtClaimTypes.Name, "ISAdmin"),
                            new Claim(JwtClaimTypes.Email, "isadmin@mail.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", System.Security.Claims.ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.Role, ISRoles.Admin),
                            new Claim(JwtClaimTypes.Scope, $"{StandardScopes.OpenId} {StandardScopes.Email} {JwtClaimTypes.Name} " +
                                $"{JwtClaimTypes.Role}, {ISClients.ISAdminPanelClientId}")
                        }).Result;

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Console.WriteLine($"{ISAdminUsername} created");
                    }

                    #endregion

                    #region BookingAdmin

                    var BookingAdmin = userMgr.FindByNameAsync(BookingAdminUsername).Result;

                    if (BookingAdmin != null)
                    {
                        Console.WriteLine($"{BookingAdminUsername} already exists");
                    }

                    if (BookingAdmin == null)
                    {
                        BookingAdmin = new ApplicationUser
                        {
                            UserName = BookingAdminUsername
                        };
                        var result = userMgr.CreateAsync(BookingAdmin, BookingAdminPassword).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        if (!roleMgr.RoleExistsAsync(BookingRoles.Admin).Result)
                        {
                            result = roleMgr.CreateAsync(new IdentityRole(BookingRoles.Admin)).Result;
                            if (!result.Succeeded)
                            {
                                throw new Exception(result.Errors.First().Description);
                            }
                        }

                        result = userMgr.AddToRoleAsync(BookingAdmin, BookingRoles.Admin).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(BookingAdmin, new Claim[]{
                            new Claim(JwtClaimTypes.Subject, BookingAdminSubject),
                            new Claim(JwtClaimTypes.Name, "BookingAdmin"),
                            new Claim(JwtClaimTypes.Email, "bookingadmin@mail.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", System.Security.Claims.ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.Role, BookingRoles.Admin),
                            new Claim(JwtClaimTypes.Scope, $"{StandardScopes.OpenId} {StandardScopes.Email} " +
                                $"{JwtClaimTypes.Name} {JwtClaimTypes.Role} " +
                                $"{ISClients.BookingAdminPanelClientId} {ISClients.BookingAPIClientId}")
                        }).Result;

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Console.WriteLine($"{BookingAdminUsername} created");
                    }

                    #endregion
                    trx.Commit();
                }
                Console.WriteLine("Transactoin finished");
            }
        }

        private static void EnsureSeedISSetup(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var grantCtx = scope.ServiceProvider.GetService<PersistedGrantDbContext>())
                {
                    grantCtx.Database.Migrate();
                }

                using (var configCtx = scope.ServiceProvider.GetService<ConfigurationDbContext>())
                {
                    configCtx.Database.Migrate();

                    using (var trx = configCtx.Database.BeginTransaction())
                    {
                        Console.WriteLine("Seeding database...");

                        if (!configCtx.Clients.Any())
                        {
                            Console.WriteLine("Clients being populated");
                            foreach (var client in Config.GetClients().ToList())
                            {
                                configCtx.Clients.Add(client.ToEntity());
                            }
                            configCtx.SaveChanges();
                        }
                        else
                        {
                            Console.WriteLine("Clients already populated");
                        }

                        if (!configCtx.IdentityResources.Any())
                        {
                            Console.WriteLine("IdentityResources being populated");
                            foreach (var resource in Config.GetIdentityResources().ToList())
                            {
                                configCtx.IdentityResources.Add(resource.ToEntity());
                            }
                            configCtx.SaveChanges();
                        }
                        else
                        {
                            Console.WriteLine("IdentityResources already populated");
                        }

                        if (!configCtx.ApiResources.Any())
                        {
                            Console.WriteLine("ApiResources being populated");
                            foreach (var resource in Config.GetApiResources().ToList())
                            {
                                configCtx.ApiResources.Add(resource.ToEntity());
                            }
                            configCtx.SaveChanges();
                        }
                        else
                        {
                            Console.WriteLine("ApiResources already populated");
                        }
                        trx.Commit();
                        Console.WriteLine("Done seeding database.");
                    }
                }
                
                Console.WriteLine("Transactoin finished");
            }
        }
    }
}
