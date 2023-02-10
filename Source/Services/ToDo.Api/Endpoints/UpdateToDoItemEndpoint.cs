using Ardalis.ApiEndpoints;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDo.Core.Application.Features;
using ToDo.Framework.ToDoClient;
using ToDo.Framework.ToDoClient.Contracts;

namespace ToDo.Api.Endpoints;

public sealed class UpdateToDoItemEndpoint : EndpointBaseAsync.WithRequest<UpdateToDoItemRequest>.WithActionResult
{
    private readonly ISender _sender;
    private readonly IValidator<UpdateToDoItemRequest> _validator;

    public UpdateToDoItemEndpoint(ISender sender, IValidator<UpdateToDoItemRequest> validator)
    {
        _sender = sender;
        _validator = validator;
    }

    [HttpPut(ToDoClientConstants.PutTodo)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = "Updates a ToDoItem",
        Description = "Updates a ToDoItem",
        OperationId = "ToDoItem.Update",
        Tags = new[] {"ToDoItems"})]
    public override async Task<ActionResult> HandleAsync([FromBody] UpdateToDoItemRequest request, CancellationToken ct = default)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }

        await _sender.Send(new UpdateToDoItemCommand(request.ToDoItemId, request.Body.Text), ct);

        return Ok();
    }
}