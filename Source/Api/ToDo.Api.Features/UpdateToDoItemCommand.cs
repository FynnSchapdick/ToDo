using MediatR;

namespace ToDo.Api.Features;

public sealed record UpdateToDoItemCommand(Guid ToDoItemId, string Text) : IRequest<Unit>;