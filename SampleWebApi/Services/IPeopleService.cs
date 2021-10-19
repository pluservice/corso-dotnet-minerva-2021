using SampleWebApi.Models;
using System.Threading.Tasks;

namespace SampleWebApi.Services
{
    public interface IPeopleService
    {
        Task SaveAsync(Person person);
    }
}