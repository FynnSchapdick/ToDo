using ToDo.Api.Core.Dtos;

namespace ToDo.Api.Contracts.Responses;

public sealed class GetPaginatedToDoItemsResponse
{
    public IEnumerable<ToDoItemDto> Data { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public bool HasPrevious { get; set; }
    public bool HasNext { get; set; }
    public int TotalCount { get; set; }
    
    public GetPaginatedToDoItemsResponse(PaginatedList<ToDoItemDto> data)
    {
        Data = data.ToList();
        CurrentPage = data.CurrentPage;
        PageSize = data.PageSize;
        TotalCount = data.TotalCount;
        TotalPages = data.TotalPages;
        HasNext = data.HasNext;
        HasPrevious = data.HasPrevious;
    }   
}