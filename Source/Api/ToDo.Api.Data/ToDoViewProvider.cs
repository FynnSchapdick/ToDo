using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using ToDo.Api.Core.Abstractions;
using ToDo.Api.Core.Domain;
using ToDo.Api.Core.Dtos;

namespace ToDo.Api.Data;

public sealed class ToDoViewProvider : IToDoViewProvider
{
    private readonly ToDoContext _context;
    private readonly ToDoItemSieveProcessor _sieveProcessor;

    public ToDoViewProvider(ToDoContext context, ToDoItemSieveProcessor sieveProcessor)
    {
        _context = context;
        _sieveProcessor = sieveProcessor;
    }

    public async Task<PaginatedList<ToDoItemDto>> GetPaginatedToDoItems(SieveModel sieveModel, CancellationToken cancellationToken)
    {
        IQueryable<ToDoItem> queryable = _context.ToDoItems.AsNoTracking();
        int count = await _sieveProcessor.Apply(sieveModel, queryable, applyPagination: false).CountAsync(cancellationToken);
        IEnumerable<ToDoItemDto> items = (await _sieveProcessor.Apply(sieveModel, queryable).ToListAsync(cancellationToken)).Select(x => new ToDoItemDto(x.ToDoItemId, x.Text, x.Status.ToString()));
        return new PaginatedList<ToDoItemDto>(items, count, sieveModel.Page, sieveModel.PageSize);
    }
}