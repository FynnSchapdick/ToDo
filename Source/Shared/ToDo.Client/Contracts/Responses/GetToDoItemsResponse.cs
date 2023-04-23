using ToDo.Client.Contracts.Dtos;

namespace ToDo.Client.Contracts.Responses;

public sealed record GetToDoItemsResponse
{
    public IEnumerable<ToDoItemDto> Data { get; init; }
    public GetToDoItemsResponse() { }
}