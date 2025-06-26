
using NewProject.Application.Common.Models;
using NewProject.Application.Employees.Commands.CreateEmployee;
using NewProject.Application.Employees.Commands.DeleteEmployee;
using NewProject.Application.Employees.Commands.UpdateEmployee;
using NewProject.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NewProject.Application.Employees.Queries;

namespace NewProject.Web.Endpoints;

public class Employee : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetEmployeesWithPagination)
            .MapGet(GetEmployeeById, "{id}")
            .MapPut(UpdateEmployee, "{id}")
            .MapDelete(DeleteEmployee, "{id}")
            .MapPost(CreateEmployee);
    }

    public async Task<Created<int>> CreateEmployee(ISender sender, CreateEmployeeCommand command)
    {
        int id = await sender.Send(command);
        return TypedResults.Created($"/{nameof(Employee)}/{id}", id);
    }

    public async Task<IResult> GetEmployeesWithPagination(ISender sender, [AsParameters] GetEmployeesWithPagination query)
    {
        var res = await sender.Send(query);
        if (!res.Success)
            return TypedResults.BadRequest(res); 

        return TypedResults.Ok(res); 
    }

    public async Task<IResult> GetEmployeeById(ISender sender, int id)
    {
        var res = await sender.Send(new GetEmployeeByIdQuery(id));

        if (!res.Success)
            return TypedResults.NotFound(res);

        return TypedResults.Ok(res);
    }

    public async Task<Results<NoContent, BadRequest>> UpdateEmployee(ISender sender, int id, UpdateEmployeeCommand command)
    {

        if (id != command.Id) { return TypedResults.BadRequest(); }


        var res = await sender.Send(command);
        return TypedResults.NoContent();


    }

    public async Task<NoContent> DeleteEmployee(ISender sender, int id)
    {
        await sender.Send(new DeleteEmployeeCommand(id));

        return TypedResults.NoContent();
    }
}


