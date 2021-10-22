using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SampleWebApi.DataAccessLayer;
using SampleWebApi.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApi.BusinessLayer.Services
{
    public class PeopleService : IPeopleService, IDisposable//, IAsyncDisposable
    {
        private readonly DataContext dataContext;
        private readonly IMapper mapper;

        public PeopleService(DataContext dataContext, IMapper mapper)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Person>> GetListAsync()
        {
            var people = await dataContext.People
                .OrderBy(p => p.FirstName).ThenBy(p => p.LastName)
                //.Select(p => p.ToDto())
                .ProjectTo<Person>(mapper.ConfigurationProvider)
                .ToListAsync();

            return people;
        }

        public async Task<Person> GetAsync(int id)
        {
            var dbPerson = await dataContext.People.FindAsync(id);
            if (dbPerson != null)
            {
                var person = mapper.Map<Person>(dbPerson);
                return person;
            }

            return null;

            //return dbPerson?.ToDto();
        }

        public async Task<Person> SaveAsync(SavePersonRequest person)
        {
            var dbPerson = person.Id > 0 ? await dataContext.FindAsync<DataAccessLayer.Entities.Person>(person.Id)
                : null;

            if (dbPerson == null)
            {
                dbPerson = mapper.Map<DataAccessLayer.Entities.Person>(person);
                dbPerson.CreatedAt = DateTime.UtcNow;

                dataContext.People.Add(dbPerson);
            }
            else
            {
                mapper.Map(person, dbPerson);
            }

            await dataContext.SaveChangesAsync();

            var result = mapper.Map<Person>(dbPerson);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            var dbPerson = await dataContext.FindAsync<DataAccessLayer.Entities.Person>(id);
            if (dbPerson != null)
            {
                dataContext.People.Remove(dbPerson);
                await dataContext.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }

        public async ValueTask DisposeAsync()
        {
            await Task.Delay(1000);
        }
    }
}
