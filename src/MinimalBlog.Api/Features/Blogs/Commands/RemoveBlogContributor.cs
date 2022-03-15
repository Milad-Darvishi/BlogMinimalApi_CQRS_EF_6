using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalBlog.Dal;

namespace MinimalBlog.Api.Features.Blogs.Commands;

public static class RemoveBlogContributor
{
    public class Command : IRequest
    {
        public int BlogId { get; init; }
        public int ContributorId { get; init; }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly MinimalBlogDbContext _context;

        public Handler(MinimalBlogDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var blog = await _context.Blogs.Include(b => b.Contributors)
                .FirstOrDefaultAsync(b => b.Id == request.BlogId, cancellationToken);

            var author =
                await _context.Authors.FirstOrDefaultAsync(a => a.Id == request.ContributorId, cancellationToken);

            if (blog != null && author != null)
            {
                blog.Contributors.Remove(author);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}