using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using FluentValidation;

namespace Application.Features.Tasks.Validators
{
    public class UpdateTaskValidator : AbstractValidator<UpdateTaskDto>
    {
        public UpdateTaskValidator()
        {
            RuleFor(t => t.DueDate).GreaterThan(DateTime.UtcNow).WithMessage("Error Due date must be after creation date");
        }
    }
}
