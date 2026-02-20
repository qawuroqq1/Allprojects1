/// <summary>
/// </summary>
namespace OrdersService.Mappings
{
    using AutoMapper;
    using OrdersService.DTOs;
    using OrdersService.Models;

    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<OrderEntity, OrderDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            this.CreateMap<OrderDto, OrderEntity>()
                .ForMember(dest => dest.Status, opt => opt.Ignore());
        }
    }
}