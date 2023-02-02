using MediatR;
using ToDo.Core.Abstractions;
using ToDo.Core.Domain;

namespace ToDo.Core.Application.Features;

public sealed class CreateToDoItemCommandHandler : IRequestHandler<CreateToDoItemCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateToDoItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Guid> Handle(CreateToDoItemCommand command, CancellationToken cancellationToken)
    {
        ToDoItem toDoItem = await _unitOfWork.ToDoItems.Add(command.Text, cancellationToken);
        await _unitOfWork.CommitChanges(cancellationToken);
        return toDoItem.Id;
    }
}