using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewProject.Application.Common.Interfaces;
using NewProject.Application.Common.Models;

namespace NewProject.Application.Employees.Queries;
public record GetEmployeeByIdQuery(int Id) : IRequest<ResponseResult<EmployeeDto>>;

public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, ResponseResult<EmployeeDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ResponseResult<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Employees
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            return ResponseResult<EmployeeDto>.Failure("Employee not found.");
        }

        var dto = _mapper.Map<EmployeeDto>(entity);
        return ResponseResult<EmployeeDto>.SuccessResult(dto, "Employee fetched successfully.");
    }
}
