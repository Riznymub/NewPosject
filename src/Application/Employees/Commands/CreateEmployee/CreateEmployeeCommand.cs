using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewProject.Application.Common.Interfaces;
using NewProject.Domain.Entities;

namespace NewProject.Application.Employees.Commands.CreateEmployee;
public record CreateEmployeeCommand:IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Department { get; set; } = string.Empty;
}

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
{
    public readonly IApplicationDbContext _context;
    public CreateEmployeeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee
        {
            Name = request.Name,
            Age = request.Age,
            Department = request.Department
        };

        //employee.AddDomainEvent(new EmployeeCreatedEvent(employee));

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync(cancellationToken);

        return employee.Id;
    }
}
