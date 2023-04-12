namespace ToDo.Api.IntegrationTests.EndpointTests;

public sealed class PatchToDoItemStatusEndpointTests : BaseToDoItemEndpointTests
{
    public PatchToDoItemStatusEndpointTests(ToDoApiFactory apiFactory) : base(apiFactory)
    {
    }
    
    [Fact]
    public async Task PatchStatus_ReturnsOK_WhenRequestIsValid()
    {
        // Arrange
        await Factory.ArrangeForEndpointTesting(ToDoItems.First());
        
        // Act
        IApiResponse response = await Client.PatchToDoItemStatus(ToDoItems.First().ToDoItemId, new("Completed"));

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }
    
    [Theory]
    [InlineData("InvalidStatus")]
    [InlineData("")]
    [InlineData(null)]
    public async Task PatchStatus_ReturnsBadRequest_WhenRequestedStatusIsInvalid(string invalidStatusText)
    {
        // Arrange
        await Factory.ArrangeForEndpointTesting(ToDoItems.First());
        
        // Act
        IApiResponse response = await Client.PatchToDoItemStatus(ToDoItems.First().ToDoItemId, new(invalidStatusText));
        
        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest,response.StatusCode);
    }
    
    [Fact]
    public async Task PatchStatus_ReturnsBadRequest_WhenRequestedIdIsInvalid()
    {
        // Act
        IApiResponse response = await Client.PatchToDoItemStatus(Guid.Empty, new("Completed"));
        
        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest,response.StatusCode);
    }
    
    [Fact]
    public async Task Patch_ReturnsNotFound_WhenEntityDoesNotExists()
    {
        // Act
        IApiResponse response = await Client.PatchToDoItemStatus(Guid.NewGuid(), new("Completed"));
        
        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
    }
}