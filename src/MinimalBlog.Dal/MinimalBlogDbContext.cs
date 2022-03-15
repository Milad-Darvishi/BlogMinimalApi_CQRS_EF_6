using Microsoft.EntityFrameworkCore;
using MinimalBlog.Domain.Model;

namespace MinimalBlog.Dal;

public class MinimalBlogDbContext : DbContext
{
    public MinimalBlogDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Article> Articles { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Author> Authors { get; set; } = default!;
    public DbSet<Blog> Blogs { get; set; } = default!;
}