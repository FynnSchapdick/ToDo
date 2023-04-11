using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Api.Contracts.Requests;
using ToDo.Api.Core.Abstractions;
using ToDo.Api.Core.Domain;
using ToDo.Api.Data;
using ToDo.Api.Filters;

namespace ToDo.Api.Endpoints;

public static class PatchToDoItemStatusEndpoints
{
    private const string PatchToDoItemStatusRoute = "todoitems/{toDoItemId}/status";
    private const string ContentType = "application/json";
    
    public static void MapPatchToDoItemStatusEndpoints(this WebApplication app)
    {
        app.MapPatch(PatchToDoItemStatusRoute, PatchToDoItemStatus)
            .Accepts<PatchToDoItemStatusRequestBody>(ContentType)
            .Produces((int) HttpStatusCode.OK)
            .AddEndpointFilter<ValidatorFilter<PatchToDoItemStatusRequestBody>>();
    }
    
    private static async Task<IResult> PatchToDoItemStatus([FromRoute] Guid toDoItemId , PatchToDoItemStatusRequestBody body, ToDoContext context, IUnitOfWork unitOfWork, CancellationToken cancellationToken = default)
    {
        ToDoItem? toDoItem = await context.ToDoItems.AsNoTracking().FirstOrDefaultAsync(x => x.ToDoItemId == toDoItemId, cancellationToken);
        if (toDoItem is null)
        {
            return Results.NotFound(new { ToDoItemId = toDoItemId });
        }

        context.Update(toDoItem with
        {
            Status = body.Status
        });
        
        await unitOfWork.CommitChangesAsync(cancellationToken);
        return Results.Ok();
    }
}