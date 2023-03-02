namespace ToDo.Shared.Contracts.V1.Responses;

public sealed class GetPaginatedToDosResponse : PaginatedApiResponse<ToDoItemDto>
{
    public GetPaginatedToDosResponse(PaginatedList<ToDoItemDto> data) : base(data)
    {
    }

    /// <summary>
    /// Needed public constructor for supporting serialization/deserialization
    /// </summary>
    public GetPaginatedToDosResponse()
    {
    }
}