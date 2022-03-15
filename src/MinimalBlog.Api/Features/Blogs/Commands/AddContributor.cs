using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalBlog.Dal;

namespace MinimalBlog.Api.Features.Blogs.Commands;

public static class AddContributor
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

            var contributor =
                await _context.Authors.FirstOrDefaultAsync(a => a.Id == request.ContributorId, cancellationToken);

            if (blog != null && contributor != null)
            {
                if (!blog.Contributors.Any(c => c.Id == contributor.Id))
                {
                    blog.Contributors.Add(contributor);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}