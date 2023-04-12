using FluentValidation;
using ToDo.Api.Contracts.Requests;

namespace ToDo.Api.Validators;

public sealed class PatchToDoItemStatusRequestValidator : AbstractValidator<PatchToDoItemStatusRequest>
{
    public PatchToDoItemStatusRequestValidator()
    {
        RuleFor(x => x.ToDoItemId)
            .NotNull()
            .NotEmpty();
    }
}