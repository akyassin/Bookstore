using Bookstore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using System;


namespace Bookstore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateHostBuilder(args).Build();

            RunMigration(webHost);

            webHost.Run();
        }

        private static void RunMigration(IHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<BookstoreDbContext>();
                db.Database.Migrate();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    var con = builder.Build();
                    var keyVaultEndpoint = con.GetValue<string>("KeyVaultUrl");
                    builder.AddAzureKeyVault(new Uri(keyVaultEndpoint), new DefaultAzureCredential(true));
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
