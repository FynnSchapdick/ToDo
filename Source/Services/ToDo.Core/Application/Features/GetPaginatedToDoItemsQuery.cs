using MediatR;
using Sieve.Models;
using ToDo.Framework.ToDoClient.Contracts;

namespace ToDo.Core.Application.Features;

public sealed record GetPaginatedToDoItemsQuery(SieveModel SieveModel)
    : IRequest<PaginatedList<ToDoItemDto>>;