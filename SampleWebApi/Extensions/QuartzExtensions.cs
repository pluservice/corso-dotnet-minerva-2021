using Quartz;

namespace SampleWebApi.Extensions
{
    public static class QuartzExtensions
    {
        public static IServiceCollectionQuartzConfigurator AddScheduledJob<T>(this IServiceCollectionQuartzConfigurator quartz, string cronSchedule) where T : IJob
        {
            var jobKey = typeof(T).FullName;
            quartz.AddJob<T>(options => options.WithIdentity(jobKey));

            quartz.AddTrigger(options =>
            {
                options.ForJob(jobKey).WithIdentity($"{jobKey}-trigger")
                    .WithCronSchedule(cronSchedule);
            });

            return quartz;
        }
    }
}
