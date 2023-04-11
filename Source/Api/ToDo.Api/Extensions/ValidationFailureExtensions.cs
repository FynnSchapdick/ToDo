using FluentValidation.Results;
using ToDo.Api.Contracts.Responses;

namespace ToDo.Api.Extensions;

public static class ValidationFailureExtensions
{
    public static ValidationFailureResponse ToResponse(this IEnumerable<ValidationFailure> failures)
    {
        return new ValidationFailureResponse
        {
            Errors = failures.Select(x => x.ErrorMessage)
        };
    }
}