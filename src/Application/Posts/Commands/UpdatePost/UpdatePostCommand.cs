using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewProject.Application.Common.Interfaces;
using NewProject.Application.Common.Models;

namespace NewProject.Application.Posts.Commands.UpdatePost;
public class UpdatePostCommand:IRequest<ResponseResult<PostDto>>
{
    public int Id { get; set; }
    public string Tital { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, ResponseResult<PostDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public UpdatePostCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ResponseResult<PostDto>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _context.Posts.FindAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, post);

        post.Tital = request.Tital;
        post.Description = request.Description;

        _context.Posts.Update(post);
        await _context.SaveChangesAsync(cancellationToken);

        var savedPost = await _context.Posts
            .Include(p => p.employee)
            .FirstOrDefaultAsync(p => p.Id == post.Id, cancellationToken);


        var postDto = _mapper.Map<PostDto>(savedPost);

        return ResponseResult<PostDto>.SuccessResult(postDto, "Post updated successfully");
    }
}
