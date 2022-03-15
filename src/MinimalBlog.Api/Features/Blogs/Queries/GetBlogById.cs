using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalBlog.Api.Features.Blogs.Models;
using MinimalBlog.Dal;

namespace MinimalBlog.Api.Features.Blogs.Queries;

public static class GetBlogById
{
    public class Query : IRequest<BlogDto>
    {
        public int BlogId { get; set; }
    }

    public class Handler : IRequestHandler<Query, BlogDto>
    {
        private readonly MinimalBlogDbContext _context;
        private readonly IMapper _mapper;

        public Handler(MinimalBlogDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BlogDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == request.BlogId, cancellationToken);
            return _mapper.Map<BlogDto>(blog);
        }
    }
}