using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalBlog.Api.Features.Blogs.Models;
using MinimalBlog.Dal;
using MinimalBlog.Domain.Model;

namespace MinimalBlog.Api.Features.Blogs.Commands;

public static class CreateBlog
{
    public class Command : IRequest<BlogDto>
    {
        public BlogCreateDto NewBlog { get; set; } = default!;
    }

    public class Handler : IRequestHandler<Command, BlogDto>
    {
        private readonly MinimalBlogDbContext _context;
        private readonly IMapper _mapper;

        public Handler(MinimalBlogDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BlogDto> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var blog =
                new Blog
                {
                    Name = request.NewBlog.Name,
                    Description = request.NewBlog.Description,
                    AuthorId = request.NewBlog.AuhtorId,
                    CreatedDate = DateTime.UtcNow
                };

            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync(cancellationToken);
            var author = await _context.Authors.FirstAsync(a => a.Id == blog.AuthorId, cancellationToken);
            blog.Owner = author;

            return _mapper.Map<BlogDto>(blog);
        }
    }
}