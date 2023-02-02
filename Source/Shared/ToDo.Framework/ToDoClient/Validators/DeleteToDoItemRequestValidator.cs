using FluentValidation;
using ToDo.Framework.ToDoClient.Contracts;

namespace ToDo.Framework.ToDoClient.Validators;

public sealed class DeleteToDoItemRequestValidator : AbstractValidator<DeleteToDoItemRequest>
{
    public DeleteToDoItemRequestValidator()
    {
        RuleFor(request => request.ToDoItemId)
            .NotNull()
            .NotEmpty();
    }
}