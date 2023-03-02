using ToDo.Api.Domain;

namespace ToDo.Api.Data.Repositories.Abstractions;

public interface IToDoItemRepository
{
    Task<ToDoItem> Add(string text, CancellationToken ct);

    Task ToggleComplete(Guid toDoItemId, CancellationToken ct);
    
    Task Update(Guid toDoItemId, string text, CancellationToken ct);
    
    Task Delete(Guid toDoItemId, CancellationToken ct);
}