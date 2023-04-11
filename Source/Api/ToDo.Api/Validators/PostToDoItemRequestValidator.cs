using FluentValidation;
using ToDo.Api.Contracts.Requests;

namespace ToDo.Api.Validators;

public sealed class PostToDoItemRequestValidator : AbstractValidator<PostToDoItemRequest>
{
    private const int TextMinLength = 5;
    private const int TextMaxLength = 255;
    
    public PostToDoItemRequestValidator()
    {
        RuleFor(request => request.Text)
            .NotNull()
            .NotEmpty()
            .Length(TextMinLength, TextMaxLength);
    }
}