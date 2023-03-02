namespace ToDo.Api.Data.Repositories.Abstractions;

public interface IUnitOfWork
{
    IToDoItemRepository ToDoItems { get; }

    Task CommitChanges(CancellationToken ct);
}