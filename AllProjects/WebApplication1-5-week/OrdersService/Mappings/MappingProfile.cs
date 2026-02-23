/// <summary>
/// Профиль AutoMapper для преобразования сущностей заказа и DTO.
/// </summary>
namespace OrdersService.Mappings
{
    using AutoMapper;
    using OrdersService.DTOs;
    using OrdersService.Models;

    /// <summary>
    /// Настройки маппинга между OrderEntity и OrderDto.
    /// </summary>
    public sealed class MappingProfile : Profile
    {
        /// <summary>
        /// Инициализирует новый экземпляр профиля маппинга.
        /// </summary>
        public MappingProfile()
        {
            this.CreateMap<OrderEntity, OrderDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            this.CreateMap<OrderDto, OrderEntity>()
                .ForMember(dest => dest.Status, opt => opt.Ignore());
        }
    }
}