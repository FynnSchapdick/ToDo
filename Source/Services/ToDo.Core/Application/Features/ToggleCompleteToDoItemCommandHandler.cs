using MediatR;
using ToDo.Core.Abstractions;

namespace ToDo.Core.Application.Features;

public sealed class ToggleCompleteToDoItemCommandHandler : IRequestHandler<ToggleCompleteToDoItemCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public ToggleCompleteToDoItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(ToggleCompleteToDoItemCommand request, CancellationToken ct)
    {
        await _unitOfWork.ToDoItems.ToggleComplete(request.ToDoItemId, ct);
        await _unitOfWork.CommitChanges(ct);
        return Unit.Value;
    }
}