using FluentAssertions;
using MediatR;
using Moq;
using ToDoApi.Application.Features;
using ToDoApi.Domain;
using ToDoApi.Domain.Interfaces;
using ToDoApi.UnitTests.DomainTests;

namespace ToDoApi.UnitTests.ApplicationTests;

public sealed class UpdateToDoItemHandlerTests
{
    private readonly UpdateToDoItemCommandHandler _handler;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    public UpdateToDoItemHandlerTests()
    {
        _handler = new UpdateToDoItemCommandHandler(_unitOfWorkMock.Object);
    }

    [Theory]
    [ClassData(typeof(ValidUpdateToDoItemTestData))]
    public async Task UpdateToDoItemCommandHandler_Handle_ShouldHandle(ToDoItem toDoItem, string text)
    {
        UpdateToDoItemCommand command = new(toDoItem.Id, text);
        CancellationToken cancellationToken = new();
        
        _unitOfWorkMock.Setup(x => x.ToDoItems.Update(toDoItem.Id, text, cancellationToken))
            .ReturnsAsync(new ToDoItem
            {
                Id = toDoItem.Id,
                Completed = false,
                Text = text
            });

        _unitOfWorkMock.Setup(x => x.CommitChanges(cancellationToken))
            .Returns(Task.CompletedTask);
        
        Unit result = await _handler.Handle(command, cancellationToken);
        result.Should().NotBeNull();
    }
}