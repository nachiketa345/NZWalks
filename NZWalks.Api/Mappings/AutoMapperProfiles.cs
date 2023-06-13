using AutoMapper;
using NZWalks.Api.Models.Domains;
using NZWalks.Api.Models.DTO;

namespace NZWalks.Api.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<RegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateDtoRequest, Region>().ReverseMap();
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            CreateMap<UpdateWalkDtoRequest,  Walk>().ReverseMap();
            
        }
    }
}
