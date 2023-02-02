using MediatR;

namespace ToDo.Core.Application.Features;

public sealed record ToggleCompleteToDoItemCommand(Guid ToDoItemId) : IRequest<Unit>;