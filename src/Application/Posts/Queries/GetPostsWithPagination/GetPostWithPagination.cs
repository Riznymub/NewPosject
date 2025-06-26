using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewProject.Application.Common.Interfaces;
using NewProject.Application.Common.Mappings;
using NewProject.Application.Common.Models;

namespace NewProject.Application.Posts.Queries.GetPostsWithPagination;
public class GetPostWithPagination:IRequest<ResponseResult<PaginatedList<PostDto>>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class GetPostWithPaginationHandler : IRequestHandler<GetPostWithPagination, ResponseResult<PaginatedList<PostDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetPostWithPaginationHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ResponseResult<PaginatedList<PostDto>>> Handle(GetPostWithPagination request, CancellationToken cancellationToken)
    {
        var pagedList = await _context.Posts
            .AsNoTracking()
            .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return ResponseResult<PaginatedList<PostDto>>.SuccessResult(pagedList, "Posts fetched successfully.");
    }
}
