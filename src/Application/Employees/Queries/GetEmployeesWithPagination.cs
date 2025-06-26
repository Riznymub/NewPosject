using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using NewProject.Application.Common.Interfaces;
using NewProject.Application.Common.Mappings;
using NewProject.Application.Common.Models;
using NewProject.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NewProject.Application.Employees.Queries;
public record GetEmployeesWithPagination:IRequest<ResponseResult<PaginatedList<EmployeeDto>>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class GetEmployeesWithPaginationHandler : IRequestHandler<GetEmployeesWithPagination, ResponseResult<PaginatedList<EmployeeDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetEmployeesWithPaginationHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    async Task<ResponseResult<PaginatedList<EmployeeDto>>> IRequestHandler<GetEmployeesWithPagination, ResponseResult<PaginatedList<EmployeeDto>>>.Handle(GetEmployeesWithPagination request, CancellationToken cancellationToken)
    {
        var pagedList = await _context.Employees
            .AsNoTracking()
            .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return ResponseResult<PaginatedList<EmployeeDto>>.SuccessResult(pagedList, "Employees fetched successfully.");
    }
}
