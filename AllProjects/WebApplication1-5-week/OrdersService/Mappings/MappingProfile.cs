using OrdersService.Domain.DTOs;

namespace OrdersService.Mappings;

using AutoMapper;
using OrdersService.Models;

/// <summary>
/// Профиль AutoMapper для преобразования сущностей и DTO.
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<OrderEntity, OrderDto>();
        CreateMap<OrderDto, OrderEntity>();
    }
}