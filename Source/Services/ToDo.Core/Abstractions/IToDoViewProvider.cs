using Sieve.Models;
using ToDo.Core.Domain;
using ToDo.Framework.ToDoClient.Contracts;

namespace ToDo.Core.Abstractions;

public interface IToDoViewProvider
{
    Task<PaginatedList<ToDoItem>> GetPaginatedTodoItems(SieveModel sieveModel, CancellationToken ct);
}