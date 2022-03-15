using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalBlog.Dal;

namespace MinimalBlog.Api.Features.Blogs.Commands;

public static class UpdateBlogInfo
{
    public class Command : IRequest
    {
        public int BlogId { get; init; }
        public string Name { get; init; } = default!;
        public string? Description { get; init; }
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
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == request.BlogId, cancellationToken);

            if (blog != null)
            {
                blog.Name = request.Name;
                blog.Description = request.Description ?? "";
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}