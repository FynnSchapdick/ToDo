using Ardalis.ApiEndpoints;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDo.Api.Features;
using ToDo.Shared.Constants;
using ToDo.Shared.Contracts.V1.Requests;

namespace ToDo.Api.Endpoints;

public sealed class DeleteToDoItemEndpoint : EndpointBaseAsync.WithRequest<DeleteToDoItemRequest>.WithActionResult
{
    private readonly ISender _sender;
    private readonly IValidator<DeleteToDoItemRequest> _validator;

    public DeleteToDoItemEndpoint(ISender sender, IValidator<DeleteToDoItemRequest> validator)
    {
        _sender = sender;
        _validator = validator;
    }

    [HttpDelete(ToDoConstants.DeleteTodo)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = "Deletes a ToDoItem",
        Description = "Deletes a ToDoItem",
        OperationId = "ToDoItem.Delete",
        Tags = new[] {"ToDoItems"})]
    public override async Task<ActionResult> HandleAsync([FromRoute] DeleteToDoItemRequest request, CancellationToken ct = default)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }

        await _sender.Send(new DeleteToDoItemCommand(request.ToDoItemId), ct);

        return Ok();
    }
}