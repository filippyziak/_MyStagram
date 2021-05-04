using System.Threading.Tasks;
using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using MyStagram.Core.Helpers;
using MyStagram.Core.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using MyStagram.Core.Logging;

namespace MyStagram.API.BackgroundServices
{
    public class ServerHostedService : IDisposable, IHostedService
    {
        private readonly INLogger logger;
        private readonly IServiceProvider service;

        private Timer timer;
        public ServerHostedService(INLogger logger, IServiceProvider service)
        {
            this.logger = logger;
            this.service = service;

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.Info("Background server hosted service started...");
            timer = new Timer(Reload, null, TimeSpan.Zero, TimeSpan.FromDays(Constants.ServerHostedServiceTimeInDays));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.Info("Background server hosted service stopped...");
            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        private async void Reload(object state)
        {
            using (var scope = service.CreateScope())
            {
                var logManager = scope.ServiceProvider.GetRequiredService<ILogManager>();
                var storyManager = scope.ServiceProvider.GetRequiredService<IStoryService>();

                await logManager.StoreLogs();
                await logManager.ClearLogs();

                await storyManager.ClearStories();
                logger.Info("Background server hosted service invoked");
            }
        }
    }
}