using AutoMapper;
using MinimalBlog.Api.Features.Blogs.Models;
using MinimalBlog.Domain.Model;

namespace MinimalBlog.Api.Features.Blogs.Profiles;

public class BlogsProfile : Profile
{
    public BlogsProfile()
    {
        CreateMap<Author, BlogAuthorDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio));

        CreateMap<Blog, BlogDto>();

        CreateMap<Author, ContributorDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
            .ForMember(dest => dest.ContributorId, opt => opt.MapFrom(src => src.Id));
    }
}