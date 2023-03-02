using MediatR;
using ToDo.Api.Data.Repositories.Abstractions;
using ToDo.Shared.Contracts.V1.Responses;

namespace ToDo.Api.Features;

public sealed class GetPaginatedToDoItemsQueryHandler : IRequestHandler<GetPaginatedToDoItemsQuery, PaginatedList<ToDoItemDto>>
{
    private readonly IToDoViewProvider _viewProvider;

    public GetPaginatedToDoItemsQueryHandler(IToDoViewProvider viewProvider)
    {
        _viewProvider = viewProvider;
    }

    public async Task<PaginatedList<ToDoItemDto>> Handle(GetPaginatedToDoItemsQuery query, CancellationToken ct)
        => await _viewProvider.GetPaginatedTodoItems(query.SieveModel, ct);
}