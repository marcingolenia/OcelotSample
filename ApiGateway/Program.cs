using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGateway
{
    public class Program
    {
        private static bool InDocker => Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

        public static void Main(string[] args)
        {
            new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var environment = InDocker
                        ? "docker"
                        : hostingContext.HostingEnvironment.EnvironmentName.ToLower();
                    config
                        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", true)
                        .AddJsonFile($"appsettings.{environment}.json".ToLower(), true)
                        .AddJsonFile($"ocelot.{environment}.json".ToLower(), true)
                        .AddEnvironmentVariables();
                })
                .ConfigureServices(services =>
                {
                    services.AddLogging(configure => configure.AddConsole());
                    services.AddOcelot().AddSingletonDefinedAggregator<ClientsInvoicesAggregator>();
                })
                .Configure(app => { app.UseOcelot().Wait(); })
                .Build()
                .Run();
        }
    }
}