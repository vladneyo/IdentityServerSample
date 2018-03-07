using System;
using System.Collections.Generic;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServerSample.Host
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = services.AddIdentityServer();
            builder.AddInMemoryPersistedGrants();
            builder.AddInMemoryClients(new List<Client> { });
            builder.AddInMemoryIdentityResources(new List<IdentityResource> { });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseIdentityServer();
        }
    }
}
