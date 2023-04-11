using ToDo.Client.Contracts.Dtos;

namespace ToDo.Client.Contracts.Responses;

public sealed class GetPaginatedToDoItemsResponse
{
    public IEnumerable<ToDoItemDto> Data { get; init; }
    public int CurrentPage { get; init; }
    public int PageSize { get; init; }
    public int TotalPages { get; init; }
    public bool HasPrevious { get; init; }
    public bool HasNext { get; init; }
    public int TotalCount { get; init; }

    public GetPaginatedToDoItemsResponse()
    {
    }
}