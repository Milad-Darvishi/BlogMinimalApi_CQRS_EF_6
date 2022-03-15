namespace MinimalBlog.Api.Features.Blogs.Models;

public record BlogAuthorDto
{
    public string Name { get; init; } = default!;
    public string? Bio { get; init; }
}