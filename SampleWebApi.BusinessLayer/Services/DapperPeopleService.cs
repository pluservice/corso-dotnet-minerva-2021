using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SampleWebApi.DataAccessLayer;
using SampleWebApi.Shared.Models;

namespace SampleWebApi.BusinessLayer.Services
{
    public class DapperPeopleService : IPeopleService
    {
        private readonly ISqlContext sqlContext;

        public DapperPeopleService(ISqlContext sqlContext)
        {
            this.sqlContext = sqlContext;
        }

        public async Task DeleteAsync(int id)
        {
            await sqlContext.Connection.ExecuteAsync("DELETE FROM People WHERE ID = @id", new { id });
        }

        public async Task<Person> GetAsync(int id)
        {
            var person = await sqlContext.Connection
                .QueryFirstOrDefaultAsync<Person>("SELECT Id, FirstName + ' ' + LastName AS Name, City FROM People WHERE ID = @id",
                new { id = id });

            return person;
        }

        public async Task<IEnumerable<Person>> GetListAsync()
        {
            var people = await sqlContext.Connection
                .QueryAsync<Person>("SELECT Id, FirstName + ' ' + LastName AS Name, City FROM People ORDER BY FirstName, LastName");

            return people;
        }

        public Task<Person> SaveAsync(SavePersonRequest person)
        {
            throw new NotImplementedException();
        }
    }
}
