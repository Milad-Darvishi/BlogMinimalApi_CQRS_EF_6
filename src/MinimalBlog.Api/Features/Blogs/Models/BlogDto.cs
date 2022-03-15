using MinimalBlog.Domain.Model;

namespace MinimalBlog.Api.Features.Blogs.Models;

public record BlogDto
{
    public int Id { get; init; }
    public string Name { get; init; } = default!;
    public string? Description { get; init; }
    public DateTime DateCreated { get; init; }
    public Author Owner { get; init; } = default!;
}