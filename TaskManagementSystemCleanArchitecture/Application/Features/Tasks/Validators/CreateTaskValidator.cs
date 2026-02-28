using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Application.DTOs;
namespace Application.Features.Tasks.Validators
{
    public class CreateTaskValidator : AbstractValidator<CreateTaskDto>
    {
        public CreateTaskValidator() 
        {
        
            RuleFor(t => t.Title).NotEmpty().WithMessage("Task title is required")
                .MaximumLength(100).WithMessage("Task title shouldn't be more than 100 characters");

            RuleFor(t => t.DueDate).GreaterThan(DateTime.UtcNow).WithMessage("Error Due date must be after creation date");
        }
    }
}
