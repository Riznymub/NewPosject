using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Application.Posts.Commands.CreatePost;
public class CreatePostCommandValidation:AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidation()
    {
        RuleFor(p=>p.EmployeeId).NotEmpty();
        RuleFor(p => p.Tital).NotEmpty().MaximumLength(500);
        RuleFor(p=>p.Description).NotEmpty().MaximumLength(1000);
    }
}
