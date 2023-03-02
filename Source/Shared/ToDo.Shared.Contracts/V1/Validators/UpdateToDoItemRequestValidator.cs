using FluentValidation;
using ToDo.Shared.Contracts.V1.Requests;

namespace ToDo.Shared.Contracts.V1.Validators;

public sealed class UpdateToDoItemRequestValidator : AbstractValidator<UpdateToDoItemRequest>
{
    public UpdateToDoItemRequestValidator()
    {
        RuleFor(request => request.ToDoItemId)
            .NotNull()
            .NotEmpty();

        RuleFor(request => request.Body)
            .NotNull();
        
        RuleFor(request => request.Body.Text)
            .NotNull()
            .NotEmpty()
            .Length(5, 255);
    }
}