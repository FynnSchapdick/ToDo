using ToDo.Api.Core.Dtos;

namespace ToDo.Api.Contracts.Responses;

public sealed class GetToDoItemsResponse
{
    public IEnumerable<ToDoItemDto> Data { get; set; }

    public GetToDoItemsResponse(IEnumerable<ToDoItemDto> data)
    {
        Data = data.ToList();
    }   
}