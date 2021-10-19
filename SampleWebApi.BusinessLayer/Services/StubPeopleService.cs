using Microsoft.Extensions.Logging;
using SampleWebApi.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleWebApi.BusinessLayer.Services
{
    public class StubPeopleService : IPeopleService, IDisposable
    {
        public StubPeopleService(ILogger<PeopleService> logger)
        {
        }

        public Task<IEnumerable<Person>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public Task<Person> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Person> SaveAsync(SavePersonRequest person)
        {
            throw new NotImplementedException();
        }
    }
}
