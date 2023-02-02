using FluentAssertions;
using Moq;
using ToDoApi.Application.Features;
using ToDoApi.Domain;
using ToDoApi.Domain.Interfaces;

namespace ToDoApi.UnitTests.ApplicationTests;

public sealed class CreateToDoItemHandlerTests
{
    private readonly CreateToDoItemCommandHandler _handler;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    public CreateToDoItemHandlerTests()
    {
        _handler = new CreateToDoItemCommandHandler(_unitOfWorkMock.Object);
    }

    [InlineData("Dies ist ein Text")]
    [Theory]
    public async Task CreateToDoItemCommandHandler_Handle_ShouldHandle(string text)
    {
        CreateToDoItemCommand command = new(text);
        CancellationToken cancellationToken = new();

        _unitOfWorkMock.Setup(x => x.ToDoItems.Add(text, cancellationToken))
            .ReturnsAsync(ToDoItem.Create(text));

        _unitOfWorkMock.Setup(x => x.CommitChanges(cancellationToken))
            .Returns(Task.CompletedTask);

        Guid id = await _handler.Handle(command, cancellationToken);
        id.Should().NotBeEmpty();
    }
}