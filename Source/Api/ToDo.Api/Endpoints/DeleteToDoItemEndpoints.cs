using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ToDo.Api.Contracts.Requests;
using ToDo.Api.Core.Abstractions;
using ToDo.Api.Core.Domain;
using ToDo.Api.Data;
using ToDo.Api.Filters;

namespace ToDo.Api.Endpoints;

public static class DeleteToDoItemEndpoints
{
    private const string DeleteToDoItemRoute = "/todoitems/{toDoItemId}";

    public static void MapDeleteToDoItemEndpoints(this WebApplication app)
    {

        app.MapDelete(DeleteToDoItemRoute, DeleteToDoItem)
            .Produces((int) HttpStatusCode.OK)
            .Produces((int) HttpStatusCode.NotFound)
            .AddEndpointFilter<ValidatorFilter<DeleteToDoItemRequest>>();
    }
    
    private static async Task<IResult> DeleteToDoItem([AsParameters] DeleteToDoItemRequest request, ToDoContext context, IUnitOfWork unitOfWork, CancellationToken cancellationToken = default)
    {
        ToDoItem? toDoItem = await context.ToDoItems.AsNoTracking().FirstOrDefaultAsync(x => x.ToDoItemId == request.ToDoItemId, cancellationToken);
        if (toDoItem is null)
        {
            return Results.NotFound(new { ToDoItemId = request.ToDoItemId});
        }

        context.ToDoItems.Remove(toDoItem);
        await unitOfWork.CommitChangesAsync(cancellationToken);
        return Results.Ok();
    }
}