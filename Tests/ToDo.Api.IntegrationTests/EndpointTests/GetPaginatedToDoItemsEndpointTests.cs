using ToDo.Client.Contracts.Responses;

namespace ToDo.Api.IntegrationTests.EndpointTests;

public sealed class GetPaginatedToDoItemsEndpointTests : BaseToDoItemEndpointTests
{
    public GetPaginatedToDoItemsEndpointTests(ToDoApiFactory apiFactory) : base(apiFactory)
    {
    }
    
    [Theory]
    [InlineData(1, 100)]
    public async Task GetPaginated_ReturnsOKWithPaginatedResult_WhenRequestIsValid(int page, int pageSize)
    {
        // Arrange
        await Factory.ArrangeForEndpointTesting(ToDoItems);
        
        // Act
        ApiResponse<GetPaginatedToDoItemsResponse> response = await Client.GetPaginatedToDoItems(page, pageSize);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(response.Content);
        Assert.Equal(page, response.Content.CurrentPage);
        Assert.Equal(pageSize, response.Content.PageSize);
        Assert.NotEmpty(response.Content.Data);
    }
}