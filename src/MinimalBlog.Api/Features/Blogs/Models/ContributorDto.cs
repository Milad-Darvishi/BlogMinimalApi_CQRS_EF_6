namespace MinimalBlog.Api.Features.Blogs.Models;

public record ContributorDto
{
    public int ContributorId { get; init; }
    public string Name { get; init; } = default!;
    public string? Bio { get; init; }
}