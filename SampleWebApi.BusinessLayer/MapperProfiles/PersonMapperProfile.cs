using AutoMapper;
using SampleWebApi.Shared.Models;

namespace SampleWebApi.BusinessLayer.MapperProfiles
{
    public class PersonMapperProfile : Profile
    {
        public PersonMapperProfile()
        {
            CreateMap<DataAccessLayer.Entities.Person, Person>()
                .ForMember(dst => dst.Name,
                    opt => opt.MapFrom(source => $"{source.FirstName} {source.LastName}"));
            //.ForMember(dst => dst.City,
            //    opt => opt.MapFrom(source => source.Address.City));

            CreateMap<SavePersonRequest, DataAccessLayer.Entities.Person>();
        }
    }
}
