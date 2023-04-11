using Sieve.Models;
using ToDo.Api.Core.Dtos;

namespace ToDo.Api.Core.Abstractions;

public interface IToDoViewProvider
{
    Task<PaginatedList<ToDoItemDto>> GetPaginatedToDoItems(SieveModel sieveModel, CancellationToken cancellationToken);
}