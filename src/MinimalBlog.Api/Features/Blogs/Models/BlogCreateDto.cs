namespace MinimalBlog.Api.Features.Blogs.Models;

public record BlogCreateDto(string Name, string Description, int AuhtorId);