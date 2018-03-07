using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace IdentityServerSample.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "IdentityServer";

            BuildWebHost(args).Run();

            Console.WriteLine("Identity Server started!");
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
