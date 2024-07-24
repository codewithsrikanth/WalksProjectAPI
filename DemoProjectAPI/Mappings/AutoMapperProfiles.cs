using AutoMapper;
using DemoProjectAPI.Models.Domain;
using DemoProjectAPI.Models.DTO;

namespace DemoProjectAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //CreateMap<UserDTO, UserDomain>()
            //    .ForMember(x=>x.Name, opt=> opt.MapFrom(x=>x.FullName))
            //    .ReverseMap();

            CreateMap<Region,RegionDto>().ReverseMap(); 
            CreateMap<AddRegionRequestDto,Region>().ReverseMap(); 
            CreateMap<UpdateRegionRequestDto,Region>().ReverseMap(); 
        }
    }

   
}
