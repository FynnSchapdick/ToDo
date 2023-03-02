using FluentValidation;
using ToDo.Shared.Contracts.V1.Requests;

namespace ToDo.Shared.Contracts.V1.Validators;

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