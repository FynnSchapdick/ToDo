namespace ToDo.Api.IntegrationTests.EndpointTests;

public sealed class PostToDoItemEndpointTests : BaseToDoItemEndpointTests
{
    public PostToDoItemEndpointTests(ToDoApiFactory apiFactory) : base(apiFactory)
    {
    }

    [Fact]
    public async Task Post_ReturnsCreated_WhenRequestIsValid()
    {
        // Act
        IApiResponse response = await Client.PostToDoItem(new(ValidText));
        
        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created,response.StatusCode);
    }

    [Fact]
    public async Task Post_ReturnsBadRequest_WhenRequestIsNotValid()
    {
        // Act
        IApiResponse response = await Client.PostToDoItem(new(InvalidText));
        
        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest,response.StatusCode);
    }
}