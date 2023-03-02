using ToDo.Shared.Contracts.V1.Responses;

namespace ToDo.Api.Endpoints;

public sealed class GetPaginatedToDoItemsResponse : PaginatedApiResponse<ToDoItemDto>
{
    public GetPaginatedToDoItemsResponse(PaginatedList<ToDoItemDto> data) : base(data)
    {
    }   
}