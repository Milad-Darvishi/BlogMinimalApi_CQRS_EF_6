namespace MinimalBlog.Api.Features.Authors.Models;

public record AuthorDto(string FirstName, string LastName, DateTime DateOfBirth, string? Bio);