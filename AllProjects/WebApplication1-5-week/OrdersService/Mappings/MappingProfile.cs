// <copyright file="MappingProfile.cs" company="AllProjects">
// Copyright (c) AllProjects. All rights reserved.
// </copyright>

namespace OrdersService.Mappings
{
    using AutoMapper;
    using OrdersService.DTOs;
    using OrdersService.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<OrderEntity, OrderDto>().ReverseMap();
        }
    }
}