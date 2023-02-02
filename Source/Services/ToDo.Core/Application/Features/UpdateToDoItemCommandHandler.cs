using MediatR;
using ToDo.Core.Abstractions;

namespace ToDo.Core.Application.Features;

public sealed class UpdateToDoItemCommandHandler : IRequestHandler<UpdateToDoItemCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateToDoItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Unit> Handle(UpdateToDoItemCommand request, CancellationToken ct)
    {
        await _unitOfWork.ToDoItems.Update(request.ToDoItemId, request.Text, ct);
        await _unitOfWork.CommitChanges(ct);
        return Unit.Value;
    }
}