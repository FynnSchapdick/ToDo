namespace ToDo.Api.IntegrationTests.EndpointTests;

public sealed class DeleteToDoItemEndpointTests : BaseToDoItemEndpointTests
{
    public DeleteToDoItemEndpointTests(ToDoApiFactory apiFactory) : base(apiFactory)
    {
    }

    [Fact]
    public async Task Delete_ReturnsOK_WhenRequestIsValid()
    {
        // Arrange 
        await Factory.ArrangeForEndpointTesting(ToDoItems.First());
        
        // Act
        IApiResponse response = await Client.DeleteToDoItem(ToDoItems.First().ToDoItemId);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsBadRequest_WhenRequestIsNotValid()
    {
        // Act
        IApiResponse response = await Client.DeleteToDoItem(Guid.Empty);
        
        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest,response.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenEntityDoesNotExists()
    {
        // Act
        IApiResponse response = await Client.DeleteToDoItem(Guid.NewGuid());
        
        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
    }
}