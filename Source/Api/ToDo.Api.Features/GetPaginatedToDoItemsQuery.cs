using MediatR;
using Sieve.Models;
using ToDo.Shared.Contracts.V1.Responses;

namespace ToDo.Api.Features;

public sealed record GetPaginatedToDoItemsQuery(SieveModel SieveModel)
    : IRequest<PaginatedList<ToDoItemDto>>;