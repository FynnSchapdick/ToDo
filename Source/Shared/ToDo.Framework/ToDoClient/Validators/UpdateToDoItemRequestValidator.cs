using FluentValidation;
using ToDo.Framework.ToDoClient.Contracts;

namespace ToDo.Framework.ToDoClient.Validators;

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