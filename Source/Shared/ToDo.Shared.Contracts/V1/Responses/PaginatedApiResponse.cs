namespace ToDo.Shared.Contracts.V1.Responses;

public abstract class PaginatedApiResponse<TDto>
{
    public IEnumerable<TDto> Data { get; set; } = null!;
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public bool HasPrevious { get; set; }
    public bool HasNext { get; set; }
    public int TotalCount { get; set; }

    /// <summary>
    /// Needed public constructor for supporting serialization/deserialization on inherting types
    /// </summary>
    protected PaginatedApiResponse() { }

    protected PaginatedApiResponse(PaginatedList<TDto> data)
    {
        Data = data.ToList();
        CurrentPage = data.CurrentPage;
        PageSize = data.PageSize;
        TotalCount = data.TotalCount;
        TotalPages = data.TotalPages;
        HasNext = data.HasNext;
        HasPrevious = data.HasPrevious;
    }
    
    public IEnumerable<TModel> Transform<TModel>(Func<TDto, TModel> transformer) where TModel : class
        => Data.Any()
            ? Data.Select(transformer)
            : Enumerable.Empty<TModel>();
}

