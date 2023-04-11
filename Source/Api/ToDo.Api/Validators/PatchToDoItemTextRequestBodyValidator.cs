using FluentValidation;
using ToDo.Api.Contracts.Requests;

namespace ToDo.Api.Validators;

public sealed class PatchToDoItemTextRequestBodyValidator : AbstractValidator<PatchToDoItemTextRequestBody>
{
    private const int TextMinLength = 5;
    private const int TextMaxLength = 255;
    
    public PatchToDoItemTextRequestBodyValidator()
    {
        RuleFor(request => request.Text)
            .NotNull()
            .NotEmpty()
            .Length(TextMinLength, TextMaxLength);
    }
}