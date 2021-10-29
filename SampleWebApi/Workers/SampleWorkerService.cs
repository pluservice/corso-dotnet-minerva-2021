using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SampleWebApi.DataAccessLayer;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SampleWebApi.Workers
{
    public class SampleWorkerService : BackgroundService
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<SampleWorkerService> logger;
        private readonly IServiceProvider serviceProvider;

        public SampleWorkerService(IConfiguration configuration, ILogger<SampleWorkerService> logger,
            IServiceProvider serviceProvider)
        {
            this.configuration = configuration;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    logger.LogDebug("Esecuzione alle: {DateTime}.", DateTime.Now);

                    using var scope = serviceProvider.CreateScope();
                    var dataContext = scope.ServiceProvider.GetService<DataContext>();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                }
                finally
                {
                    await Task.Delay(TimeSpan.FromSeconds(5));
                }
            }
        }
    }
}
