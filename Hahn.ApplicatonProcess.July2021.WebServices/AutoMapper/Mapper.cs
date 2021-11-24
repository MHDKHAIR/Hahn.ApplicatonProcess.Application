using AutoMapper;
using Hahn.ApplicatonProcess.July2021.Domain.Dtos;
using Hahn.ApplicatonProcess.July2021.Domain.Entities;

namespace Hahn.ApplicatonProcess.July2021.WebServices.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, CreateUserResposeDto>();
            CreateMap<CreateUserDto, User>().ReverseMap();
        }
    }
}
