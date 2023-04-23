using ToDo.Client.Contracts.Responses;

namespace ToDo.Api.IntegrationTests.EndpointTests;

public sealed class GetToDoItemsEndpointTests : BaseToDoItemEndpointTests
{
    public GetToDoItemsEndpointTests(ToDoApiFactory apiFactory) : base(apiFactory)
    {
    }
    
    [Fact]
    public async Task GetToDoItems_ReturnsOKWithResponseData_WhenRequestIsValid()
    {
        // Arrange
        await Factory.ArrangeForEndpointTesting(ToDoItems);
        
        // Act
        ApiResponse<GetToDoItemsResponse> response = await Client.GetToDoItems();

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(response.Content);
        Assert.NotEmpty(response.Content.Data);
    }
}