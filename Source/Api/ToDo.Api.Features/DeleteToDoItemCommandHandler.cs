using MediatR;
using ToDo.Api.Data.Repositories.Abstractions;

namespace ToDo.Api.Features;

public sealed class DeleteToDoItemCommandHandler : IRequestHandler<DeleteToDoItemCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteToDoItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteToDoItemCommand command, CancellationToken ct)
    {
        await _unitOfWork.ToDoItems.Delete(command.ToDoItemId, ct);
        await _unitOfWork.CommitChanges(ct);
        return Unit.Value;
    }
}