using System.Net;
using ToDo.Infrastructure.Entities;

namespace ToDo.Api.IntegrationTests.EndpointTests;

public sealed class UpdateToDoItemEndpointTests : IClassFixture<ToDoApiFactory>
{
    private readonly ToDoApiFactory _apiFactory;
    private readonly IToDoClient _client;

    private readonly ToDoItemEntity _toDoItem = new Faker<ToDoItemEntity>()
        .CustomInstantiator(faker => ToDoItemEntity.Create(faker.Random.String2(5, 255))).Generate();

    public UpdateToDoItemEndpointTests(ToDoApiFactory apiFactory)
    {
        _apiFactory = apiFactory;
        _client = RestService.For<IToDoClient>(apiFactory.CreateClient());
    }

    [Fact]
    public async Task Update_ReturnsOK_WhenRequestIsValid()
    {
        await _apiFactory.InsertToDoItemForEndpointTesting(_toDoItem);
        IApiResponse response = await _client.UpdateToDoItem(_toDoItem.Id, new("I was second Text."));
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Update_ReturnsBadRequest_WhenRequestIsNotValid()
    {
        IApiResponse response = await _client.UpdateToDoItem(Guid.Empty, new("I was second Text."));
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenEntityNotExists()
    {
        IApiResponse response = await _client.UpdateToDoItem(Guid.NewGuid(), new("I was second Text."));
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}