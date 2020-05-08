using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace WebApi
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            await host.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
           WebHost
               .CreateDefaultBuilder(args)
               .ConfigureAppConfiguration((context, configuration) =>
               {
                   configuration.AddEnvironmentVariables();

                   if (!context.HostingEnvironment.IsDevelopment() && context.HostingEnvironment.EnvironmentName != "Test")
                   {
                       configuration.AddSecretsManager(configurator: options =>
                       {
                           options.KeyGenerator = (secret, name) => name.Replace("__", ":");
                       });

                       configuration.AddSystemsManager(Environment.GetEnvironmentVariable("SystemManagerPath"), TimeSpan.FromMinutes(5));
                       configuration.AddSystemsManager(Environment.GetEnvironmentVariable("SystemManagerPathShared"), TimeSpan.FromMinutes(5));
                   }
               })
               .UseSerilog((context, configuration) =>
               {
                   configuration.Enrich.FromLogContext();
                   configuration.Enrich.WithExceptionDetails();
                   configuration.Enrich.WithMachineName();
                   configuration.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning);
                   configuration.MinimumLevel.Override("MassTransit", Serilog.Events.LogEventLevel.Debug);
                   configuration.WriteTo.Console();
               })
               .UseStartup<Startup>();
    }
}