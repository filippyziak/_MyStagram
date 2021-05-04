using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyStagram.Infrastructure.Database;
using MyStagram.Core.Models.Domain.Auth;
using NLog.Web;
using MyStagram.Core.Helpers;
using MyStagram.API.BackgroundServices.Interfaces;

namespace MyStagram.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var logger = NLogBuilder.ConfigureNLog(Constants.NlogConfig).GetCurrentClassLogger();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var dataContext = services.GetRequiredService<DataContext>();
                    var DatabaseManager = services.GetRequiredService<IDatabaseManager>();
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var roleManager = services.GetRequiredService<RoleManager<Role>>();

                    dataContext.Database.Migrate();

                    DatabaseManager.Seed();

                    logger.Debug("Application initialized...");

                    host.Run();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occured during migration");
                    throw;
                }
                finally
                {
                    NLog.LogManager.Shutdown();
                }
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                 .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog();
    }
}
