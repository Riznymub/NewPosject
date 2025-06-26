using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewProject.Application.Common.Interfaces;
using NewProject.Application.Common.Models;
using NewProject.Domain.Entities;

namespace NewProject.Application.Posts.Commands.CreatePost;
public class CreatePostCommand:IRequest<ResponseResult<PostDto>>
{
    public string Tital { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int EmployeeId { get; set; }
}

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, ResponseResult<PostDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public CreatePostCommandHandler(IApplicationDbContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ResponseResult<PostDto>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post
        {
            Tital = request.Tital,
            Description = request.Description,
            EmployeeId = request.EmployeeId,
        };
        _context.Posts.Add(post);
        await _context.SaveChangesAsync(cancellationToken);

        var savedPost = await _context.Posts
            .Include(p => p.employee)
            .FirstOrDefaultAsync(p => p.Id == post.Id, cancellationToken);


        var postDto = _mapper.Map<PostDto>(savedPost);

        return ResponseResult<PostDto>.SuccessResult(postDto, "Post created successfully");
    }
}
