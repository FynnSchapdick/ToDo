using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ToDo.Api.Contracts.Requests;
using ToDo.Api.Core.Abstractions;
using ToDo.Api.Core.Domain;
using ToDo.Api.Data;
using ToDo.Api.Extensions;
using ToDo.Api.Filters;

namespace ToDo.Api.Endpoints;

public static class PostToDoItemEndpoints
{
    private const string PostToDoItemRoute = "todoitems";
    private const string ContentType = "application/json";
    
    public static void MapPostToDoItemEndpoints(this WebApplication app)
    {
        app.MapPost(PostToDoItemRoute, PostToDoItem)
            .Accepts<PostToDoItemRequest>(ContentType)
            .Produces((int) HttpStatusCode.Created)
            .AddEndpointFilter<ValidatorFilter<PostToDoItemRequest>>();
    }
    
    private static async Task<IResult> PostToDoItem(PostToDoItemRequest request, HttpContext httpContext, IUnitOfWork unitOfWork, ToDoContext context, CancellationToken cancellationToken = default)
    {
        ToDoItem toDoItem = new ToDoItem(request.Text);
        await context.ToDoItems.AddAsync(toDoItem, cancellationToken);
        await unitOfWork.CommitChangesAsync(cancellationToken);
        return Results.Created(httpContext.BuildRouteLocationHeaderUri(PostToDoItemRoute, toDoItem.ToDoItemId), toDoItem.ToDoItemId);
    }
}