using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewProject.Domain.Entities;

namespace NewProject.Application.Common.Models;
public class PostDto
{
    public string Tital { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int EmployeeId { get; set; }
    public EmployeeDto? employee { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Post,PostDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}
