using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewProject.Application.Common.Interfaces;
using NewProject.Application.Common.Models;
using NewProject.Application.Employees.Queries;

namespace NewProject.Application.Posts.Queries.GetPostById;
public record GetPostByIdQuery(int id) : IRequest<ResponseResult<PostDto>>;

public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, ResponseResult<PostDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPostByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ResponseResult<PostDto>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Posts
            .FirstOrDefaultAsync(e => e.Id == request.id, cancellationToken);

        if (entity == null)
        {
            return ResponseResult<PostDto>.Failure("Post not found.");
        }

        var dto = _mapper.Map<PostDto>(entity);
        return ResponseResult<PostDto>.SuccessResult(dto, "Post fetched successfully.");
    }
}
