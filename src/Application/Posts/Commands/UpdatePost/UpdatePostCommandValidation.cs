using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Application.Posts.Commands.UpdatePost;
public class UpdatePostCommandValidation: AbstractValidator<UpdatePostCommand>
{
    public UpdatePostCommandValidation()
    {
        RuleFor(p => p.Tital).NotEmpty().MaximumLength(500);
        RuleFor(p => p.Description).NotEmpty().MaximumLength(1000);
    }
}
