using Microsoft.EntityFrameworkCore;
using ToDo.Core.Abstractions;
using ToDo.Core.Domain;
using ToDo.Infrastructure.Entities;
using ToDo.Infrastructure.Exceptions;

namespace ToDo.Infrastructure.Repositories;

public sealed class ToDoItemRepository : IToDoItemRepository
{
    private readonly ToDoContext _context;
    public ToDoItemRepository(ToDoContext context)
    {
        _context = context;
    }
    
    public async Task<ToDoItem> Add(string text, CancellationToken ct)
        => (await _context.ToDoItems.AddAsync(ToDoItemEntity.Init(ToDoItem.Create(text)), ct))
            .Entity.AsModel();

    public async Task ToggleComplete(Guid toDoItemId, CancellationToken ct)
        => (await FindToDoItem(toDoItemId, ct))
            .ToggleComplete();

    public async Task Update(Guid toDoItemId, string text, CancellationToken ct)
        => (await FindToDoItem(toDoItemId, ct))
            .UpdateText(text);

    public async Task Delete(Guid toDoItemId, CancellationToken ct)
        => _context.ToDoItems.Remove(
            await _context.ToDoItems.FirstOrDefaultAsync(x => x.Id == toDoItemId, ct)
                ?? throw new ToDoItemNotFoundException($"ToDoItem with Id: {toDoItemId} not found!"));

    private async Task<ToDoItem> FindToDoItem(Guid toDoItemId, CancellationToken ct)
        => (await _context.ToDoItems.FindAsync(new object?[] { toDoItemId }, ct))?.AsModel()
           ?? throw new ToDoItemNotFoundException($"ToDoItem with Id: {toDoItemId} not found!");
}