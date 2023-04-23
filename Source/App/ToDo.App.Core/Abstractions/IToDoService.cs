using ToDo.App.Core.Domain;

namespace ToDo.App.Core.Abstractions;

public interface IToDoService
{
    Task<IEnumerable<ToDoItem>> GetToDoItems();

    Task<Guid> CreateToDoItem(string text);

    Task<bool> DeleteToDoItem(Guid id);

    Task<bool> PatchToDoItemText(Guid id, string text);

    Task<bool> PatchToDoItemStatus(Guid id, string status);
}