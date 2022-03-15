using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalBlog.Api.Features.Authors.Models;
using MinimalBlog.Dal;

namespace MinimalBlog.Api.Features.Authors.Queries;

public static class GetAllAuthors
{
    public class Query : IRequest<List<AuthorGetDto>>
    {
    }

    public class Handler : IRequestHandler<Query, List<AuthorGetDto>>
    {
        private readonly MinimalBlogDbContext _context;
        private readonly IMapper _mapper;

        public Handler(MinimalBlogDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<AuthorGetDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var authors = await _context.Authors.ToListAsync(cancellationToken);
            return _mapper.Map<List<AuthorGetDto>>(authors);
        }
    }
}