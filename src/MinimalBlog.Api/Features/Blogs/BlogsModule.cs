using MediatR;
using MinimalBlog.Api.Contracts;
using MinimalBlog.Api.Features.Blogs.Commands;
using MinimalBlog.Api.Features.Blogs.Models;
using MinimalBlog.Api.Features.Blogs.Queries;

namespace MinimalBlog.Api.Features.Blogs;

public class BlogsModule : IModule
{
    public IEndpointRouteBuilder RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/blogs", GetAllBlogs)
            .WithDisplayName("Blogs")
            .WithName("GetBlogs")
            .Produces<List<BlogDto>>();

        endpoints.MapGet("/api/blogs/{id}", GetBlogById)
            .WithName("GetBlogById")
            .WithDisplayName("Blogs")
            .Produces<BlogDto>()
            .Produces(404)
            .Produces(500);

        endpoints.MapGet("api/blogs/{blogId}/contributors", GetBlogContributors)
            .WithName("GetBlogContributors")
            .WithDisplayName("Blogs")
            .Produces<List<ContributorDto>>()
            .Produces(500);

        endpoints.MapPost("/api/blogs", CreateBlog)
            .WithName("Createblog")
            .WithDisplayName("Blogs")
            .Produces<BlogCreateDto>(201)
            .Produces(500);

        endpoints.MapPost("api/blogs/{blogId}/contributors", AddBlogContributor)
            .WithName("AddContributor")
            .WithDisplayName("Blogs")
            .Produces(204)
            .Produces(500);

        endpoints.MapDelete("api/blogs/{blogdId}/contributors/{contributorId}", RemoveContributor)
            .WithName("RemoveContributor")
            .WithDisplayName("Blogs")
            .Produces(204)
            .Produces(500);

        endpoints.MapPut("api/blogs/{blogId}/owner", UpdateOwner)
            .WithName("UpdateBlogOwner")
            .WithDisplayName("Blogs")
            .Produces(204)
            .Produces(500);

        endpoints.MapPut("api/blogs/{blogId}", UpdateInfo)
            .WithName("UpdateBlogInfo")
            .WithDisplayName("Blogs")
            .Produces(204)
            .Produces(500);

        return endpoints;
    }

    private async Task<IResult> GetAllBlogs(IMediator mediator, CancellationToken ct)
    {
        var result = await mediator.Send(new GetAllBlogs.Query(), ct);
        return Results.Ok(result);
    }

    private async Task<IResult> GetBlogById(int id, IMediator mediator, CancellationToken ct)
    {
        var result = await mediator.Send(new GetBlogById.Query { BlogId = id }, ct);
        return result is null ? Results.NotFound() : Results.Ok(result);
    }

    private async Task<IResult> GetBlogContributors(int blogId, IMediator mediator, CancellationToken ct)
    {
        var query = new GetBlogContributors.Query
        {
            BlogId = blogId
        };

        var result = await mediator.Send(query, ct);
        return Results.Ok(result);
    }

    private async Task<IResult> CreateBlog(BlogCreateDto blog, IMediator mediator, CancellationToken ct)
    {
        var command = new CreateBlog.Command { NewBlog = blog };
        var result = await mediator.Send(command, ct);
        return Results.CreatedAtRoute("GetBlogById", new { result.Id }, result);
    }

    private async Task<IResult> AddBlogContributor(int blogId, ContributorAddDto contributor, IMediator mediator,
        CancellationToken ct)
    {
        var command = new AddContributor.Command { BlogId = blogId, ContributorId = contributor.ContributorId };
        await mediator.Send(command, ct);
        return Results.NoContent();
    }

    private async Task<IResult> RemoveContributor(int blogId, int contributorId, IMediator mediator,
        CancellationToken ct)
    {
        var command = new RemoveBlogContributor.Command { BlogId = blogId, ContributorId = contributorId };
        await mediator.Send(command, ct);
        return Results.NoContent();
    }

    private async Task<IResult> UpdateOwner(int blogId, OwnerUpdateDto newOwner, IMediator mediator,
        CancellationToken ct)
    {
        var command = new UpdateBlogOwner.Command { BlogId = blogId, OwnerId = newOwner.OwnerId };
        await mediator.Send(command, ct);
        return Results.NoContent();
    }

    private async Task<IResult> UpdateInfo(int blogId, BlogInfoDto infoDto, IMediator mediator, CancellationToken ct)
    {
        var command = new UpdateBlogInfo.Command
            { BlogId = blogId, Name = infoDto.Name, Description = infoDto.Description };
        await mediator.Send(command, ct);
        return Results.NoContent();
    }
}