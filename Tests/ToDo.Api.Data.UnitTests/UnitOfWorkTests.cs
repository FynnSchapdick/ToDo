namespace ToDo.Api.Data.UnitTests;

public sealed class UnitOfWorkTests
{
    [Fact]
    public async Task CommitChangesAsync_ShouldRaiseException()
    {
        Mock<ToDoContext> mockContext = new Mock<ToDoContext>(new DbContextOptionsBuilder<ToDoContext>()
            .UseInMemoryDatabase("sampleDb")
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options);
        
        mockContext.Setup(c => c.Database).Returns(new DatabaseFacade(mockContext.Object));
        mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception());

        UnitOfWork unitOfWork = new UnitOfWork(mockContext.Object);
        await Assert.ThrowsAsync<Exception>(() => unitOfWork.CommitChangesAsync(CancellationToken.None));
        
        mockContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}