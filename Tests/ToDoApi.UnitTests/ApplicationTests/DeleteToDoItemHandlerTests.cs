using FluentAssertions;
using MediatR;
using Moq;
using ToDoApi.Application.Features;
using ToDoApi.Domain.Interfaces;

namespace ToDoApi.UnitTests.ApplicationTests;

public sealed class DeleteToDoItemHandlerTests
{
    private readonly DeleteToDoItemCommandHandler _handler;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    public DeleteToDoItemHandlerTests()
    {
        _handler = new DeleteToDoItemCommandHandler(_unitOfWorkMock.Object);
    }
    
    [Fact]
    public async Task CompleteToDoItemCommandHandler_Handle_ShouldHandle()
    {
        Guid id = Guid.NewGuid();
        DeleteToDoItemCommand command = new(id);
        CancellationToken cancellationToken = new();
        _unitOfWorkMock.Setup(x => x.ToDoItems.Delete(id, cancellationToken))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock.Setup(x => x.CommitChanges(cancellationToken))
            .Returns(Task.CompletedTask);
        
        Unit result = await _handler.Handle(command, cancellationToken);
        result.Should().NotBeNull();
    }
}