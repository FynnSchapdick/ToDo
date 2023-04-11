using FluentValidation;
using ToDo.Api.Contracts.Requests;

namespace ToDo.Api.Validators;

public sealed class DeleteToDoItemRequestValidator : AbstractValidator<DeleteToDoItemRequest>
{
    public DeleteToDoItemRequestValidator()
    {
        RuleFor(request => request.ToDoItemId)
            .NotNull()
            .NotEmpty();
    }
}