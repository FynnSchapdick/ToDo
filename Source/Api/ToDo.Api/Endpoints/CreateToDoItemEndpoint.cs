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
public sealed class CreateToDoItemEndpoint : EndpointBaseAsync.WithRequest<CreateToDoItemRequest>.WithActionResult
{
    private readonly ISender _sender;
    private readonly IValidator<CreateToDoItemRequest> _validator;

    public CreateToDoItemEndpoint(ISender sender, IValidator<CreateToDoItemRequest> validator)
    {
        _sender = sender;
        _validator = validator;
    }

    [HttpPost(ToDoConstants.PostTodo)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "Creates a ToDoItem",
        Description = "Creates a ToDoItem",
        OperationId = "ToDoItem.Create",
        Tags = new[] {"ToDoItems"})]
    public override async Task<ActionResult> HandleAsync([FromBody] CreateToDoItemRequest request, CancellationToken ct = default)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }

        Guid id = await _sender.Send(new CreateToDoItemCommand(request.Text), ct);

        return Created("", new { ToDoItemId = id }); //TODO: get uri of created object
    }
}
