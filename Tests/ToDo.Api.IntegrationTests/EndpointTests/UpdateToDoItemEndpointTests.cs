using System.Net;
using ToDo.Framework.ToDoClient.Contracts;
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
        IApiResponse response = await _client.UpdateToDoItem(new (_toDoItem.Id, new UpdateToDoItemRequestBody("I was second Text.")));
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Update_ReturnsBadRequest_WhenRequestIsNotValid()
    {
        IApiResponse response = await _client.UpdateToDoItem(new UpdateToDoItemRequest(Guid.Empty, new UpdateToDoItemRequestBody("I was second Text.")));
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenEntityNotExists()
    {
        IApiResponse response = await _client.UpdateToDoItem(new UpdateToDoItemRequest(Guid.NewGuid(), new UpdateToDoItemRequestBody("I was second Text.")));
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}