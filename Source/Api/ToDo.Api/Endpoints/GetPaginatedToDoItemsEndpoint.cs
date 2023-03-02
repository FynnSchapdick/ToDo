using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using Swashbuckle.AspNetCore.Annotations;
using ToDo.Api.Features;
using ToDo.Shared.Constants;

namespace ToDo.Api.Endpoints;

public sealed class GetPaginatedToDoItemsEndpoint : EndpointBaseAsync.WithRequest<SieveModel>.WithResult<GetPaginatedToDoItemsResponse>
{
    private const string Summary = "Get Paginated ToDoItems";
    private const string Description = "Get Paginated ToDoItems";
    private const string OperationId = "ToDoItem.PaginatedList";
    private const string FeatureTag = "ToDoItems";

    private readonly ISender _sender;

    public GetPaginatedToDoItemsEndpoint(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet(ToDoConstants.GetPaginatedTodos)]
    [SwaggerOperation(Summary = Summary, Description = Description, OperationId = OperationId, Tags = new[]{ FeatureTag })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public override async Task<GetPaginatedToDoItemsResponse> HandleAsync([FromQuery] SieveModel sieveModel, CancellationToken ct = default)
        => new(await _sender.Send(new GetPaginatedToDoItemsQuery(sieveModel), ct));
}