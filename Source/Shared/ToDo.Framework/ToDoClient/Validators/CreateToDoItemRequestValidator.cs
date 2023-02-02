using FluentValidation;
using ToDo.Framework.ToDoClient.Contracts;

namespace ToDo.Framework.ToDoClient.Validators;

public sealed class CreateToDoItemRequestValidator : AbstractValidator<CreateToDoItemRequest>
{
    public CreateToDoItemRequestValidator()
    {
        RuleFor(request => request.Text)
            .NotNull()
            .NotEmpty()
            .Length(5, 255);
    }
}