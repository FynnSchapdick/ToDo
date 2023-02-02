using MediatR;

namespace ToDo.Core.Application.Features;

public sealed record UpdateToDoItemCommand(Guid ToDoItemId, string Text) : IRequest<Unit>;