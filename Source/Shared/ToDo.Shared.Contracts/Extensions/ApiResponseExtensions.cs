using Refit;

namespace ToDo.Shared.Contracts.Extensions;

public static class ApiResponseExtensions
{
    public static IApiResponse Validate(this IApiResponse response)
        => response.IsValid() ? response : throw new Exception();
    public static ApiResponse<T> Validate<T>(this ApiResponse<T>  response) where T : class
        => response.IsValid() && response is {Content: not null} ? response : throw new Exception();
    private static bool IsValid(this IApiResponse response)
        => response is {IsSuccessStatusCode: true};
}