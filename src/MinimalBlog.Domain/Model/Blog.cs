namespace MinimalBlog.Domain.Model;

public class Blog
{
    public Blog()
    {
        Contributors = new List<Author>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime CreatedDate { get; set; }

    public int AuthorId { get; set; }
    public Author Owner { get; set; } = default!;

    public ICollection<Author> Contributors { get; }
}