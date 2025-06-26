using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Domain.Entities;
public class Post:BaseAuditableEntity
{
    public string Tital { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [ForeignKey("Employee")]
    public int EmployeeId { get; set; }
    public Employee? employee { get; set; }
}
