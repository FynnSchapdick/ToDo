using FluentValidation;
using ToDo.Framework.ToDoClient.Contracts;

namespace ToDo.Framework.ToDoClient.Validators;

public sealed class ToggleToDoItemRequestValidator : AbstractValidator<ToggleToDoItemRequest>
{
    public ToggleToDoItemRequestValidator()
    {
        RuleFor(request => request.ToDoItemId)
            .NotNull()
            .NotEmpty();
    }
}