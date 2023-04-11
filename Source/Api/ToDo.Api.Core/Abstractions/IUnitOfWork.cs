namespace ToDo.Api.Core.Abstractions;

public interface IUnitOfWork
{
    Task CommitChangesAsync(CancellationToken cancellationToken);
}