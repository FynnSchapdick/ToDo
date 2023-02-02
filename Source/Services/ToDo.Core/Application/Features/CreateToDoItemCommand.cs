using MediatR;

namespace ToDo.Core.Application.Features;

public sealed record CreateToDoItemCommand(string Text) : IRequest<Guid>;