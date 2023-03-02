using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using ToDo.Api.Data.Entities;
using ToDo.Api.Data.Repositories.Abstractions;
using ToDo.Shared.Contracts.V1.Responses;

namespace ToDo.Api.Data.Repositories;

public sealed class ToDoViewProvider : IToDoViewProvider
{
    private readonly ToDoContext _context;
    private readonly ISieveProcessor _sieveProcessor;

    public ToDoViewProvider(ToDoContext context, ISieveProcessor sieveProcessor)
    {
        _context = context;
        _sieveProcessor = sieveProcessor;
    }

    public async Task<PaginatedList<ToDoItemDto>> GetPaginatedTodoItems(SieveModel sieveModel, CancellationToken ct)
    {
        IQueryable<ToDoItemEntity> queryable = _context.ToDoItems.AsNoTracking();
        int count = await _sieveProcessor.Apply(sieveModel, queryable, applyPagination: false).CountAsync(ct);
        IEnumerable<ToDoItemDto> items = (await _sieveProcessor.Apply(sieveModel, queryable).ToListAsync(ct)).Select(x => x.AsDto());
        return new PaginatedList<ToDoItemDto>(items, count, sieveModel.Page, sieveModel.PageSize);
    }
}