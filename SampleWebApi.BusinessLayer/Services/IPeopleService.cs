using SampleWebApi.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleWebApi.BusinessLayer.Services
{
    public interface IPeopleService
    {
        Task<IEnumerable<Person>> GetListAsync();

        Task<Person> GetAsync(int id);

        Task<Person> SaveAsync(SavePersonRequest person);
    }
}