using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalBlog.Dal;

namespace MinimalBlog.Api.Features.Blogs.Commands;

public static class UpdateBlogOwner
{
    public class Command : IRequest
    {
        public int BlogId { get; init; }
        public int OwnerId { get; init; }
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
            var blog = await _context.Blogs.Include(b => b.Owner)
                .FirstOrDefaultAsync(b => b.Id == request.BlogId, cancellationToken);

            var owner = await _context.Authors.FirstOrDefaultAsync(a => a.Id == request.OwnerId, cancellationToken);

            if (blog != null && owner != null)
            {
                blog.Owner = owner;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}