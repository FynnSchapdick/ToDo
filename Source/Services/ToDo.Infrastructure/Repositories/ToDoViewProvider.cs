using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using ToDo.Core.Abstractions;
using ToDo.Core.Domain;
using ToDo.Framework.ToDoClient.Contracts;

namespace ToDo.Infrastructure.Repositories;

public sealed class ToDoViewProvider : IToDoViewProvider
{
    private readonly ToDoContext _context;
    private readonly ISieveProcessor _sieveProcessor;

    public ToDoViewProvider(ToDoContext context, ISieveProcessor sieveProcessor)
    {
        _context = context;
        _sieveProcessor = sieveProcessor;
    }

    public async Task<PaginatedList<ToDoItem>> GetPaginatedTodoItems(SieveModel sieveModel, CancellationToken ct)
        => new((
            await _sieveProcessor.Apply(sieveModel, _context.ToDoItems.AsNoTracking())
                .ToListAsync(ct))
                .Select(entity => entity.AsModel())
                .ToList(),
            await _sieveProcessor.Apply(sieveModel, _context.ToDoItems.AsNoTracking(), applyPagination: false)
                .CountAsync(ct), 
            sieveModel.Page,
            sieveModel.PageSize);
}