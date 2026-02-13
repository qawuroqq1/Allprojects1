using AutoMapper;
using OrdersService.DTOs;
using OrdersService.Models;

namespace OrdersService.Mappings;     

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<OrderEntity, OrderDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<OrderDto, OrderEntity>();
    }
}