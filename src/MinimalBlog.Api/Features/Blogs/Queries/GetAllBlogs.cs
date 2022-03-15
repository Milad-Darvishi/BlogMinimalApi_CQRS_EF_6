using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalBlog.Api.Features.Blogs.Models;
using MinimalBlog.Dal;

namespace MinimalBlog.Api.Features.Blogs.Queries;

public static class GetAllBlogs
{
    public class Query : IRequest<List<BlogDto>>
    {
    }

    public class Handler : IRequestHandler<Query, List<BlogDto>>
    {
        private readonly MinimalBlogDbContext _context;
        private readonly IMapper _mapper;

        public Handler(MinimalBlogDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<BlogDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var blogs = await _context.Blogs.Include(b => b.Owner).ToListAsync(cancellationToken);
            return _mapper.Map<List<BlogDto>>(blogs);
        }
    }
}