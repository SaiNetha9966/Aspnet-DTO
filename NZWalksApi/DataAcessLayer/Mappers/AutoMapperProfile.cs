using AutoMapper;
using NZWalksApi.DataAcessLayer.Models.Domains;
using NZWalksApi.DTO;

namespace NZWalksApi.DataAcessLayer.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionsDto , Region>().ReverseMap();
            CreateMap<UpdateRegionDto , Region>().ReverseMap();
        }
    }
}
