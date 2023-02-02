using ToDo.Framework.ToDoClient.Contracts;

namespace ToDo.Api.Endpoints;

public sealed class GetPaginatedToDoItemsResponse : PaginatedApiResponse<ToDoItemDto>
{
    public GetPaginatedToDoItemsResponse(PaginatedList<ToDoItemDto> data) : base(data)
    {
    }   
}