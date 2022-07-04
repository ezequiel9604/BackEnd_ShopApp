
using AutoMapper;
using API.DTOs;
using Domain.Entities;

namespace Infrastructure.Mapper;

public class OrderProfile : Profile
{

    public OrderProfile()
    {

        // createMpa<source, destination>
        CreateMap<OrderDTO, Order>();

        // createMpa<destination, source>
        CreateMap<OrderDTO, Order>().ReverseMap();


        // createMpa<source, destination>
        CreateMap<PurchaseDTO, Order>();

        // createMpa<destination, source>
        CreateMap<PurchaseDTO, Order>().ReverseMap();

    }

}