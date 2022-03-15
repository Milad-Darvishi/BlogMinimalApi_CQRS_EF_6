using AutoMapper;
using MinimalBlog.Api.Features.Authors.Models;
using MinimalBlog.Domain.Model;

namespace MinimalBlog.Api.Features.Authors.Profiles;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<AuthorDto, Author>().ReverseMap();
        CreateMap<Author, AuthorGetDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName));
    }
}