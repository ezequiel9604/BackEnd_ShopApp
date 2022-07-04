using AutoMapper;
using API.DTOs;
using Domain.Entities;

namespace Infrastructure.Mapper;

public class ItemProfile : Profile
{
    public ItemProfile()
    {
        // CHECK OUT

        // createMpa<destination, source>
        CreateMap<ItemDTO, Item>().ReverseMap()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name));

        // createMpa<source, destination>
        CreateMap<ItemDTO, Item>()
            .ForMember(dest => dest.Brand, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore());

        // createMpa<destination, source>
        CreateMap<ImageDTO, Image>().ReverseMap();

        // createMpa<destination, source>
        CreateMap<SubItemDTO, SubItem>().ReverseMap();

        // createMpa<destination, source>
        CreateMap<CommentDTO, Comment>().ReverseMap();

    }
}