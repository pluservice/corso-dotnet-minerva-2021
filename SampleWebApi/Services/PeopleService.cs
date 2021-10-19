using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SampleWebApi.Models;
using SampleWebApi.Settings;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SampleWebApi.Services
{
    public class PeopleService : IPeopleService, IDisposable//, IAsyncDisposable
    {
        public PeopleService(IOptionsSnapshot<ApplicationOptions> applicationOptions, IConfiguration configuration, ILogger<PeopleService> logger, IWebHostEnvironment environment)
        {
            var setting1 = applicationOptions.Value.Setting1;
        }

        public void Dispose()
        {

        }

        public async ValueTask DisposeAsync()
        {
            await Task.Delay(1000);
        }

        public async Task SaveAsync(Person person)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync(string.Empty, null);

            // ...
        }
    }
}
