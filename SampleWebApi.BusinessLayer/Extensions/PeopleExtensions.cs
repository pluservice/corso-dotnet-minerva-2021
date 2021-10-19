using SampleWebApi.Shared.Models;

namespace SampleWebApi.BusinessLayer.Extensions
{
    public static class PeopleExtensions
    {
        public static Person ToDto(this DataAccessLayer.Entities.Person source)
            => new()
            {
                Id = source.Id,
                //Name = $"{source.FirstName} {source.LastName}",
                City = source.City,
                BirthDate = source.BirthDate
            };
    }
}
