
using Microsoft.AspNetCore.Http.HttpResults;
using NewProject.Application.Employees.Commands.DeleteEmployee;
using NewProject.Application.Employees.Queries;
using NewProject.Application.Posts.Commands.CreatePost;
using NewProject.Application.Posts.Commands.DeletePost;
using NewProject.Application.Posts.Commands.UpdatePost;
using NewProject.Application.Posts.Queries.GetPostById;
using NewProject.Application.Posts.Queries.GetPostsWithPagination;

namespace NewProject.Web.Endpoints;

public class Post : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetPostWithPagination)
            .MapGet(GetPostById, "{id}")
            .MapPut(UpdatePost, "{id}")
            .MapDelete(DeletePost, "{id}")
            .MapPost(CreatePost);
    }

    public async Task<IResult> CreatePost(ISender sender, CreatePostCommand command)
    {
        var res = await sender.Send(command);
        return TypedResults.Ok(res);
    }

    public async Task<IResult> UpdatePost(ISender sender, int id, UpdatePostCommand command)
    {

        if (id != command.Id) { return TypedResults.BadRequest(); }


        var res = await sender.Send(command);
        return TypedResults.Ok(res);
    }

    public async Task<IResult> DeletePost(ISender sender, int id)
    {
        var res = await sender.Send(new DeletePostCommand(id));

        return TypedResults.Ok(res);
    }

    public async Task<IResult> GetPostWithPagination(ISender sender, [AsParameters] GetPostWithPagination query)
    {
        var res = await sender.Send(query);
        if (!res.Success)
            return TypedResults.BadRequest(res);

        return TypedResults.Ok(res);
    }

    public async Task<IResult> GetPostById(ISender sender, int id)
    {
        var res = await sender.Send(new GetPostByIdQuery(id));

        if (!res.Success)
            return TypedResults.NotFound(res);

        return TypedResults.Ok(res);
    }
}
