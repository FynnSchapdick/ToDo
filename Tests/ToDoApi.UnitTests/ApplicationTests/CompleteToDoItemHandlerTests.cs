using FluentAssertions;
using MediatR;
using Moq;
using ToDoApi.Application.Features;
using ToDoApi.Domain;
using ToDoApi.Domain.Interfaces;
using ToDoApi.UnitTests.DomainTests;

namespace ToDoApi.UnitTests.ApplicationTests;

public sealed class CompleteToDoItemHandlerTests
{
    private readonly CompleteToDoItemCommandHandler _handler;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    public CompleteToDoItemHandlerTests()
    {
        _handler = new CompleteToDoItemCommandHandler(_unitOfWorkMock.Object);
    }

    
    [Theory]
    [ClassData(typeof(CompleteToDoItemTestData))]
    public async Task CompleteToDoItemCommandHandler_Handle_ShouldHandle(ToDoItem toDoItem)
    {
        CompleteToDoItemCommand command = new(toDoItem.Id);
        CancellationToken cancellationToken = new();

        _unitOfWorkMock.Setup(x => x.ToDoItems.Complete(toDoItem.Id, cancellationToken))
            .ReturnsAsync(toDoItem);

        _unitOfWorkMock.Setup(x => x.CommitChanges(cancellationToken))
            .Returns(Task.CompletedTask);

        Unit result = await _handler.Handle(command, cancellationToken);
        result.Should().NotBeNull();
    }
}