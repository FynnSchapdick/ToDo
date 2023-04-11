using Sieve.Models;
using ToDo.Api.Contracts.Responses;
using ToDo.Api.Core.Abstractions;
using ToDo.Api.Core.Dtos;

namespace ToDo.Api.Endpoints;

public static class GetPaginatedToDoItemsEndpoints
{
    private const string GetPaginatedToDoItemsRoute = "/todoitems";
    private const string ContentType = "application/json";

    public static void MapGetPaginatedToDoItemsEndpoints(this WebApplication app)
    {
        app.MapGet(GetPaginatedToDoItemsRoute, GetPaginatedToDoItems)
            .Accepts<SieveModel>(ContentType)
            .Produces<GetPaginatedToDoItemsResponse>();
    }

    private static async Task<IResult> GetPaginatedToDoItems([AsParameters] SieveModel sieveModel, IToDoViewProvider viewProvider, CancellationToken cancellationToken = default)
    {
        PaginatedList<ToDoItemDto> paginatedList = await viewProvider.GetPaginatedToDoItems(sieveModel, cancellationToken);
        return Results.Ok(new GetPaginatedToDoItemsResponse(paginatedList));
    }
}