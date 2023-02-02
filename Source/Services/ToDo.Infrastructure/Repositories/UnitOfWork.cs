using Microsoft.Extensions.Logging;
using ToDo.Core.Abstractions;
using ToDo.Infrastructure.Exceptions;

namespace ToDo.Infrastructure.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private const string CommitFailedMessage = "Failed to commit changes";
    private readonly ToDoContext _context;
    private readonly ILogger<UnitOfWork> _logger;
    public IToDoItemRepository ToDoItems { get; }
    
    public UnitOfWork(ToDoContext context, ILogger<UnitOfWork> logger)
    {
        _context = context;
        _logger = logger;
        ToDoItems = new ToDoItemRepository(context);
    }

    public async Task CommitChanges(CancellationToken ct)
    {
        try
        {
            await _context.SaveChangesAsync(ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, CommitFailedMessage);
            throw new CommitFailedException(CommitFailedMessage);
        }
    }
}