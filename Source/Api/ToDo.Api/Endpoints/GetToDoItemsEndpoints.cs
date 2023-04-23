using System.Net;
using Microsoft.EntityFrameworkCore;
using ToDo.Api.Contracts.Responses;
using ToDo.Api.Core.Dtos;
using ToDo.Api.Data;

namespace ToDo.Api.Endpoints;

public static class GetToDoItemsEndpoints
{
    private const string GetPaginatedToDoItemsRoute = "/todoitems";

    public static void MapGetPaginatedToDoItemsEndpoints(this WebApplication app)
    {
        app.MapGet(GetPaginatedToDoItemsRoute, GetPaginatedToDoItems)
            .Produces((int) HttpStatusCode.InternalServerError)
            .Produces<GetToDoItemsResponse>();
    }

    private static async Task<IResult> GetPaginatedToDoItems(ToDoContext context, CancellationToken cancellationToken = default)
        => Results.Ok(new GetToDoItemsResponse((await context.ToDoItems.ToListAsync(cancellationToken)).Select(x => new ToDoItemDto(x.ToDoItemId, x.Text, x.Status.ToString()))));
}