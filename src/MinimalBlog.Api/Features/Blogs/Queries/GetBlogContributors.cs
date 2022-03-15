using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalBlog.Api.Features.Blogs.Models;
using MinimalBlog.Dal;

namespace MinimalBlog.Api.Features.Blogs.Queries;

public static class GetBlogContributors
{
    public class Query : IRequest<List<ContributorDto>>
    {
        public int BlogId { get; init; }
    }

    public class Handler : IRequestHandler<Query, List<ContributorDto>>
    {
        private readonly MinimalBlogDbContext _context;
        private readonly IMapper _mapper;

        public Handler(MinimalBlogDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<ContributorDto>> Handle(Query request,
            CancellationToken cancellationToken)
        {
            var blog = await _context.Blogs.Include(b => b.Contributors)
                .FirstOrDefaultAsync(b => b.Id == request.BlogId, cancellationToken);
            return blog != null ? _mapper.Map<List<ContributorDto>>(blog.Contributors) : new List<ContributorDto>();
        }
    }
}