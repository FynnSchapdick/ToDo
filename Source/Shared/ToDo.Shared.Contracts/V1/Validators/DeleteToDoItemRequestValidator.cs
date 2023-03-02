using FluentValidation;
using ToDo.Shared.Contracts.V1.Requests;

namespace ToDo.Shared.Contracts.V1.Validators;

public sealed class DeleteToDoItemRequestValidator : AbstractValidator<DeleteToDoItemRequest>
{
    public DeleteToDoItemRequestValidator()
    {
        RuleFor(request => request.ToDoItemId)
            .NotNull()
            .NotEmpty();
    }
}