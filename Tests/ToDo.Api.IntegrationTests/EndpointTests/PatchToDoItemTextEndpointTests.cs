namespace ToDo.Api.IntegrationTests.EndpointTests;

public sealed class PatchToDoItemTextEndpointTests : BaseToDoItemEndpointTests
{
    public PatchToDoItemTextEndpointTests(ToDoApiFactory apiFactory) : base(apiFactory)
    {
    }

    [Fact]
    public async Task PatchText_ReturnsOK_WhenRequestIsValid()
    {
        // Arrange
        await Factory.ArrangeForEndpointTesting(ToDoItems.First());

        // Act
        IApiResponse response = await Client.PatchToDoItemText(ToDoItems.First().ToDoItemId, new (ValidText));

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task PatchText_ReturnsBadRequest_WhenTextIsInvalid()
    {
        // Arrange
        await Factory.ArrangeForEndpointTesting(ToDoItems.First());

        // Act
        IApiResponse response = await Client.PatchToDoItemText(ToDoItems.First().ToDoItemId, new(InvalidText));

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task PatchText_ReturnsBadRequest_WhenIdIsInvalid()
    {
        // Act
        IApiResponse response = await Client.PatchToDoItemText(Guid.Empty, new(ValidText));

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PatchText_ReturnsNotFound_WhenEntityNotExists()
    {
        // Act
        IApiResponse response = await Client.PatchToDoItemText(Guid.NewGuid(), new(ValidText));

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}