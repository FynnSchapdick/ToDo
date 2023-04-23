using Refit;
using ToDo.Client.Contracts.Dtos;
using ToDo.Client.Contracts.Responses;

namespace ToDo.App.Infrastructure.Extensions;

public static class ApiResponseExtensions
{
    public static bool Validate(this IApiResponse response)
        => response.IsValid();
    public static T ValidateResponseStruct<T>(this ApiResponse<T>  response) where T : struct
        => response.IsValid() ? response.Content : throw new Exception();
    public static T ValidateResponse<T>(this ApiResponse<T>  response) where T : class
        => response.IsValid() && response.Content is not null ? response.Content : throw new Exception();
    private static bool IsValid(this IApiResponse response)
        => response is {IsSuccessStatusCode: true};

    public static IEnumerable<TModel> Transform<TModel>(this ApiResponse<GetToDoItemsResponse> response, Func<ToDoItemDto, TModel> transformer) where TModel : class
        => response.ValidateResponse().Data.Select(transformer);
}