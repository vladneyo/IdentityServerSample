using System;
using AutoMapper;
using IdentityServerSample.BookingAPI.EDM;
using IdentityServerSample.Shared.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;

namespace IdentityServerSample.BookingAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = EndpointsConstants.ISHost;
                    options.RequireHttpsMetadata = false;
                    options.Audience = ISApiNames.BookingAPI;
                });

            services.AddAutoMapper();

            services.AddDbContext<BookingContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("BookingAPIDb"));
            });

            var container = new Container();

            container.Configure(config =>
            {
                config.Scan(scan =>
                {
                    scan.AssembliesAndExecutablesFromApplicationBaseDirectory();
                    scan.TheCallingAssembly();
                    scan.LookForRegistries();
                    scan.WithDefaultConventions();
                });

                config.Populate(services);
            });

            return container.GetInstance<IServiceProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            using (var scope = app.ApplicationServices.CreateScope())
            using (var ctx = scope.ServiceProvider.GetService<BookingContext>())
            {
                ctx.Database.EnsureCreated();
            }

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
