using Microsoft.Extensions.Logging;
using Quartz;
using SampleWebApi.DataAccessLayer;
using System;
using System.Threading.Tasks;

namespace SampleWebApi.Workers
{
    [DisallowConcurrentExecution]
    public class SampleJob : IJob
    {
        private readonly ILogger<SampleJob> logger;
        private readonly DataContext dataContext;

        public SampleJob(ILogger<SampleJob> logger, DataContext dataContext)
        {
            this.logger = logger;
            this.dataContext = dataContext;
        }

        public Task Execute(IJobExecutionContext context)
        {
            logger.LogInformation("Esecuzione alle {DateTime}.", DateTime.Now);

            return Task.CompletedTask;
        }
    }
}
