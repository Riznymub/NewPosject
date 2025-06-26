using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace NewProject.Application.Employees.Commands.UpdateEmployee;
public class UpdateEmployeeCommandValidation: AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidation()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Age).NotEmpty().GreaterThan(10).LessThan(100);
        RuleFor(x => x.Department).NotEmpty().MaximumLength(200);
    }
}
