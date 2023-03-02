using MediatR;

namespace ToDo.Api.Features;

public sealed record DeleteToDoItemCommand(Guid ToDoItemId)
    : IRequest<Unit>;