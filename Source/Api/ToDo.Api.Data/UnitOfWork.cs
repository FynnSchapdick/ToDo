using Microsoft.EntityFrameworkCore.Storage;
using ToDo.Api.Core.Abstractions;

namespace ToDo.Api.Data;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ToDoContext _context;

    public UnitOfWork(ToDoContext context)
    {
        _context = context;
    }
    
    public async Task CommitChangesAsync(CancellationToken cancellationToken)
    {
        IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}