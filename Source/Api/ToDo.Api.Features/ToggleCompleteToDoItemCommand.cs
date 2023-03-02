using MediatR;

namespace ToDo.Api.Features;

public sealed record ToggleCompleteToDoItemCommand(Guid ToDoItemId) : IRequest<Unit>;