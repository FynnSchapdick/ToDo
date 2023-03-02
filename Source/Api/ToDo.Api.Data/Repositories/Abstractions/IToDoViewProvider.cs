using Sieve.Models;
using ToDo.Shared.Contracts.V1.Responses;

namespace ToDo.Api.Data.Repositories.Abstractions;

public interface IToDoViewProvider
{
    Task<PaginatedList<ToDoItemDto>> GetPaginatedTodoItems(SieveModel sieveModel, CancellationToken ct);
}