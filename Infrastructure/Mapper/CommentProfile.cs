
using AutoMapper;
using API.DTOs;
using Domain.Entities;

namespace Infrastructure.Mapper;

public class CommentProfile : Profile
{

    public CommentProfile()
    {

        // createMap<source, destination>
        CreateMap<CommentDTO, Comment>();

        // createMap<destination, source>
        CreateMap<CommentDTO, Comment>().ReverseMap();

    }

}