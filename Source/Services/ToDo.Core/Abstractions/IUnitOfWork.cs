namespace ToDo.Core.Abstractions;

public interface IUnitOfWork
{
    IToDoItemRepository ToDoItems { get; }

    Task CommitChanges(CancellationToken ct);
}