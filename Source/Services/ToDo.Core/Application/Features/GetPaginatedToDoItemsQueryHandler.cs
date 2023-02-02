using MediatR;
using ToDo.Core.Abstractions;
using ToDo.Core.Domain;
using ToDo.Framework.ToDoClient.Contracts;

namespace ToDo.Core.Application.Features;

public sealed class GetPaginatedToDoItemsQueryHandler : IRequestHandler<GetPaginatedToDoItemsQuery, PaginatedList<ToDoItemDto>>
{
    private readonly IToDoViewProvider _viewProvider;

    public GetPaginatedToDoItemsQueryHandler(IToDoViewProvider viewProvider)
    {
        _viewProvider = viewProvider;
    }

    public async Task<PaginatedList<ToDoItemDto>> Handle(GetPaginatedToDoItemsQuery query, CancellationToken ct)
    {
        PaginatedList<ToDoItem> paginatedTodoItems = await _viewProvider.GetPaginatedTodoItems(query.SieveModel, ct);
        return new PaginatedList<ToDoItemDto>(paginatedTodoItems.Select(ToDoItem.AsDto).ToList(),
            paginatedTodoItems.TotalCount, paginatedTodoItems.CurrentPage, paginatedTodoItems.PageSize);
    }
}