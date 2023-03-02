using MediatR;

namespace ToDo.Api.Features;

public sealed record CreateToDoItemCommand(string Text) : IRequest<Guid>;