using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewProject.Application.Common.Interfaces;
using NewProject.Application.Common.Models;

namespace NewProject.Application.Posts.Commands.DeletePost;
public record DeletePostCommand(int id):IRequest<Result>;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Result>
{
    public readonly IApplicationDbContext _context;

    public DeletePostCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Posts
            .FindAsync(new object[] { request.id }, cancellationToken);

        Guard.Against.NotFound(request.id, entity);

        _context.Posts.Remove(entity);


        var res = await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
