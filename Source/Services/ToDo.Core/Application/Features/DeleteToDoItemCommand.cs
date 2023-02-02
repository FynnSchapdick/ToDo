using MediatR;

namespace ToDo.Core.Application.Features;

public sealed record DeleteToDoItemCommand(Guid ToDoItemId)
    : IRequest<Unit>;