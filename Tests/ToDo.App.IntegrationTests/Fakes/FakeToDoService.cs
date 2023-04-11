using ToDo.App.Core.Abstractions;
using ToDo.App.Core.Domain;

namespace ToDo.App.IntegrationTests.Fakes;

public sealed class FakeToDoService : IToDoService
{
    private readonly List<ToDoItem> _toDoItems = new()
    {
        new(Guid.NewGuid(), "sampletext"),
        new(Guid.NewGuid(), "sampletext2"),
        new(Guid.NewGuid(), "sampletext3"),
        new(Guid.NewGuid(), "sampletext4"),
        new(Guid.NewGuid(), "sampletext5"),
        new(Guid.NewGuid(), "sampletext6"),
        new(Guid.NewGuid(), "sampletext7"),
    };
    
    public Task<IEnumerable<ToDoItem>> GetToDoItems(int page, int pageSize)
    {
        return Task.FromResult(_toDoItems.Skip((page - 1) * pageSize).Take(pageSize));
    }

    public Task<Guid> CreateToDoItem(string text)
    {
        ToDoItem item = new ToDoItem(Guid.NewGuid(), text);
        _toDoItems.Add(item);
        return Task.FromResult(item.Id);
    }

    public Task<bool> DeleteToDoItem(Guid id)
    {
        ToDoItem? toDoItem = _toDoItems.FirstOrDefault(x => x.Id == id);
        if  (toDoItem is null)
        {
            return Task.FromResult(false);
        }

        _toDoItems.Remove(toDoItem);
        return Task.FromResult(true);
    }

    public Task<bool> PatchToDoItemText(Guid id, string text)
    {
        ToDoItem? toDoItem = _toDoItems.FirstOrDefault(x => x.Id == id);
        if  (toDoItem is null)
        {
            return Task.FromResult(false);
        }

        toDoItem.Text = text;
        return Task.FromResult(true);
    }

    public Task<bool> PatchToDoItemStatus(Guid id, string status)
    {
        ToDoItem? toDoItem = _toDoItems.FirstOrDefault(x => x.Id == id);
        if  (toDoItem is null)
        {
            return Task.FromResult(false);
        }

        toDoItem.Status = status;
        return Task.FromResult(true);
    }
}