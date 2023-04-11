namespace ToDo.Api.Contracts.Responses;

public sealed class ValidationFailureResponse
{
    public IEnumerable<string> Errors { get; init; } = Enumerable.Empty<string>();
}