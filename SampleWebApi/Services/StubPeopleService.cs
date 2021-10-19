using Microsoft.Extensions.Logging;
using SampleWebApi.Models;
using System;
using System.Threading.Tasks;

namespace SampleWebApi.Services
{
    public class StubPeopleService : IPeopleService, IDisposable
    {
        public StubPeopleService(ILogger<PeopleService> logger)
        {
        }

        public void Dispose()
        {
        }

        public Task SaveAsync(Person person)
        {
            // ...
            return Task.CompletedTask;
        }
    }
}
