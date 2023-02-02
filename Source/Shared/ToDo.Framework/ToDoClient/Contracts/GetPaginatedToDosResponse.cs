namespace ToDo.Framework.ToDoClient.Contracts;

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