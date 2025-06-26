using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewProject.Domain.Entities;

namespace NewProject.Application.Common.Models;
public class EmployeeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int Age { get; set; }
    public string Department { get; set; } = default!;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}
