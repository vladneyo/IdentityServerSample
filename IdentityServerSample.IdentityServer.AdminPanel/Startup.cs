using System.IdentityModel.Tokens.Jwt;
using IdentityServerSample.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace IdentityServerSample.IdentityServer.AdminPanel
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var ISAdminPolicy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .RequireClaim("role", ISRoles.Admin)
                     .RequireAssertion(ctx => ctx.User.Claims
                            .FirstOrDefault(x => x.Type == UserClaims.AppScope)?.Value
                            .Contains(ISClients.ISAdminPanelClientId) 
                            ?? false)
                     .Build();

            services.AddMvc(config => 
            {
                config.Filters.Add(new AuthorizeFilter(ISAdminPolicy));
            });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "Cookies";
                    options.DefaultChallengeScheme = "oidc";
                })
                .AddCookie("Cookies", options =>
                {
                    options.AccessDeniedPath = "/Account/AccessDenied";
                })
                .AddOpenIdConnect("oidc", options =>
                {
                    options.SignInScheme = "Cookies";
                    options.Authority = EndpointsConstants.ISHost;
                    options.RequireHttpsMetadata = false;
                    options.ClientId = ISClients.ISAdminPanelClientId;
                    options.SaveTokens = true;

                    options.GetClaimsFromUserInfoEndpoint = true;

                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add(ISIdentityResources.Roles);
                    options.Scope.Add(ISIdentityResources.AppScopes);                    
                });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = ISAdminPolicy;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
