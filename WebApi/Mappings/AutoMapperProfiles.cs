using AutoMapper;
using WebApi.Models.Domain;
using WebApi.Models.DTO;

namespace WebApi.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();

            CreateMap<AddRegionRequestDTO, Region>().ReverseMap();

            CreateMap<UpdRegionRequest, Region>().ReverseMap();

            CreateMap<AddWalkRequestDTO, Walk>().ReverseMap();

            CreateMap<Walk, WalkDTO>().ReverseMap();
            
            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();

            CreateMap<UpdateWalkRequestDTO, Walk>().ReverseMap();
        }
    }
}
