using ToDo.App.Models;

namespace ToDo.App.Services.Abstractions;

public interface IToDoService
{
    Task<IEnumerable<ToDoItemModel>> GetToDoItems(int page, int pageSize);

    Task CreateToDoItems(string text);

    Task DeleteToDoItem(Guid id);

    Task UpdateToDoItemText(Guid id, string text);
}