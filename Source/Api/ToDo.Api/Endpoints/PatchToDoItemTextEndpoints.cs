using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Api.Contracts.Requests;
using ToDo.Api.Core.Abstractions;
using ToDo.Api.Core.Domain;
using ToDo.Api.Data;
using ToDo.Api.Filters;

namespace ToDo.Api.Endpoints;

public static class PatchToDoItemTextEndpoints
{
    private const string PatchToDoItemTextRoute = "/todoitems/{toDoItemId}/text";
    private const string ContentType = "application/json";
    
    public static void MapPatchToDoItemTextEndpoints(this WebApplication app)
    {
        app.MapPatch(PatchToDoItemTextRoute, PatchToDoItemText)
            .Accepts<PatchToDoItemTextRequestBody>(ContentType)
            .Produces((int) HttpStatusCode.OK)
            .Produces((int) HttpStatusCode.NotFound)
            .Produces((int) HttpStatusCode.BadRequest)
            .AddEndpointFilter<ValidatorFilter<PatchToDoItemTextRequest>>()
            .AddEndpointFilter<ValidatorFilter<PatchToDoItemTextRequestBody>>();
    }
    
    private static async Task<IResult> PatchToDoItemText([AsParameters] PatchToDoItemTextRequest request, [FromBody] PatchToDoItemTextRequestBody body, ToDoContext context, IUnitOfWork unitOfWork, CancellationToken cancellationToken = default)
    {
        ToDoItem? toDoItem = await context.ToDoItems.AsNoTracking().FirstOrDefaultAsync(x => x.ToDoItemId == request.ToDoItemId, cancellationToken);
        if (toDoItem is null)
        {
            return Results.NotFound(new { ToDoItemId = request.ToDoItemId });
        }

        context.Update(toDoItem with
        {
            Text = body.Text
        });
        await unitOfWork.CommitChangesAsync(cancellationToken);
        return Results.Ok();
    }
}