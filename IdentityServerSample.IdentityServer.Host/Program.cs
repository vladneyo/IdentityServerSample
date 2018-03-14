// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace IdentityServerSample.IdentityServer.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var seed = args.Any(x => x == "/seed");
            //if (seed) args = args.Except(new[] { "/seed" }).ToArray();

            var host = BuildWebHost(args);

            //if (seed)
            //{
            //    SeedData.EnsureSeedData(host.Services);
            //    return;
            //}

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>()
                    .Build();
        }
    }
}
