using Ardalis.ApiEndpoints;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDo.Core.Application.Features;
using ToDo.Framework.ToDoClient.Contracts;

namespace ToDo.Api.Endpoints;

public sealed class ToggleCompleteToDoItemEndpoint : EndpointBaseAsync.WithRequest<ToggleToDoItemRequest>.WithActionResult
{
    private readonly ISender _sender;
    private readonly IValidator<ToggleToDoItemRequest> _validator;

    public ToggleCompleteToDoItemEndpoint(ISender sender, IValidator<ToggleToDoItemRequest> validator)
    {
        _sender = sender;
        _validator = validator;
    }

    [HttpPatch("/todoitems/{ToDoItemId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = "Completes a ToDoItem",
        Description = "Completes a ToDoItem",
        OperationId = "ToDoItem.Complete",
        Tags = new[] {"ToDoItems"})]
    public override async Task<ActionResult> HandleAsync([FromRoute] ToggleToDoItemRequest request, CancellationToken ct = default)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }

        await _sender.Send(new ToggleCompleteToDoItemCommand(request.ToDoItemId), ct);

        return Ok();
    }
}