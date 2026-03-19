using AutoMapper;
using DeliveryService.Models;
using DeliveryService.ViewModels;

namespace DeliveryService.Mappings;

/// <summary>
///
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    ///
    /// </summary>
    public MappingProfile()
    {
        this.CreateMap<DeliveryOrder, DeliveryViewModel>();
    }
}