using AutoMapper;
using OrdersService.DTOs;
using OrdersService.Models;

namespace OrdersService.Mappings;

/// <summary>
/// Профиль AutoMapper для преобразования сущностей заказа и DTO.
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<OrderEntity, OrderDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<OrderDto, OrderEntity>()
            .ForMember(dest => dest.Status, opt => opt.Ignore());
    }
}