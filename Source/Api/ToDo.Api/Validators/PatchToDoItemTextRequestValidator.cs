using FluentValidation;
using ToDo.Api.Contracts.Requests;

namespace ToDo.Api.Validators;

public sealed class PatchToDoItemTextRequestValidator : AbstractValidator<PatchToDoItemTextRequest>
{
    public PatchToDoItemTextRequestValidator()
    {
        RuleFor(x => x.ToDoItemId)
            .NotNull()
            .NotEmpty();
    }
}