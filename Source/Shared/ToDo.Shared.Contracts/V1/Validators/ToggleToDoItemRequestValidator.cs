using FluentValidation;
using ToDo.Shared.Contracts.V1.Requests;

namespace ToDo.Shared.Contracts.V1.Validators;

public sealed class ToggleToDoItemRequestValidator : AbstractValidator<ToggleToDoItemRequest>
{
    public ToggleToDoItemRequestValidator()
    {
        RuleFor(request => request.ToDoItemId)
            .NotNull()
            .NotEmpty();
    }
}