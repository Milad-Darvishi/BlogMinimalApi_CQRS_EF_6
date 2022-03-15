using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalBlog.Api.Features.Authors.Models;
using MinimalBlog.Dal;

namespace MinimalBlog.Api.Features.Authors.Queries;

public static class GetAuthorById
{
    public class Query : IRequest<AuthorGetDto>
    {
        public int AuthorId { get; set; }
    }

    public class Handler : IRequestHandler<Query, AuthorGetDto>
    {
        private readonly MinimalBlogDbContext _context;
        private readonly IMapper _mapper;

        public Handler(MinimalBlogDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<AuthorGetDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == request.AuthorId, cancellationToken);
            return _mapper.Map<AuthorGetDto>(author);
        }
    }
}