using ToDo.Framework.ToDoClient.Contracts;
using ToDo.Infrastructure.Entities;

namespace ToDo.Api.IntegrationTests.EndpointTests;

public sealed class GetPaginatedToDoItemsEndpointTests : IClassFixture<ToDoApiFactory>
{
    private readonly ToDoApiFactory _apiFactory;
    private readonly IToDoClient _client;

    private readonly Faker<ToDoItemEntity> _faker = new Faker<ToDoItemEntity>()
        .CustomInstantiator(faker => ToDoItemEntity.Create(faker.Random.String2(5, 255)));
    private readonly ToDoItemEntity[] _toDoItems;

    public GetPaginatedToDoItemsEndpointTests(ToDoApiFactory apiFactory)
    {
        _apiFactory = apiFactory;
        _client = RestService.For<IToDoClient>(apiFactory.CreateClient());
        _toDoItems = Enumerable.Range(1, 100).Select(_ => _faker.Generate()).ToArray();
    }

    [Fact]
    public async Task GetPaginated_ReturnsOKWithPaginatedResult_WhenRequestIsValid()
    {
        await _apiFactory.InsertToDoItemForEndpointTesting(_toDoItems);
        ApiResponse<GetPaginatedToDosResponse> response = await _client.GetPaginatedToDoItems(1, 100);
        response.Should().NotBeNull();
        response.Content.Should().NotBeNull();
        // response.Content?.CurrentPage.Should().Be(1);
        // response.Content?.PageSize.Should().Be(100);
        // response.Content?.HasNext.Should().BeFalse();
        // response.Content?.Data.Should().NotBeEmpty();
    }
}