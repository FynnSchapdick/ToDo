using FluentValidation;
using ToDo.Api.Contracts.Requests;

namespace ToDo.Api.Validators;

public sealed class PatchToDoItemStatusRequestBodyValidator : AbstractValidator<PatchToDoItemStatusRequestBody>
{
    private const string IncompleteStatus = "Incomplete";
    private const string CompletedStatus = "Completed";
    private readonly string[] _allowedStatusValues = { IncompleteStatus, CompletedStatus };
    private const string ErrorMessage = "Status must be either '{IncompleteStatus}' or '{CompletedStatus}'";

    public PatchToDoItemStatusRequestBodyValidator()
    {
        RuleFor(request => request.Status)
            .NotNull()
            .NotEmpty()
            .Must(status => _allowedStatusValues.Contains(status))
            .WithMessage(ErrorMessage);
    }
}