namespace ToDo.App.Infrastructure.Services.Abstractions;

public interface IPaginatedApiResponse<TDto>
{
    public IEnumerable<TDto> Data { get; init; }
    public int CurrentPage { get; init; }
    public int PageSize { get; init; }
    public int TotalPages { get; init; }
    public bool HasPrevious { get; init; }
    public bool HasNext { get; init; }
    public int TotalCount { get; init; }
}