using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Domain.Entities;
public class Employee:BaseAuditableEntity
{
    public string Name { get; set; } = default!;
    public int Age { get; set; }
    public string Department { get; set; } = default!;
}
