using System.Net;
using ToDo.Api.Data.Entities;
using ToDo.App.Infrastructure.Services.Abstractions;

namespace ToDo.Api.IntegrationTests.EndpointTests;

public sealed class ToggleCompleteToDoItemEndpointTests : IClassFixture<ToDoApiFactory>
{
    private readonly ToDoApiFactory _apiFactory;
    private readonly IToDoClient _client;
    private readonly ToDoItemEntity _toDoItem = new Faker<ToDoItemEntity>()
        .CustomInstantiator(faker => ToDoItemEntity.Create(faker.Random.String2(5, 255))).Generate();

    public ToggleCompleteToDoItemEndpointTests(ToDoApiFactory apiFactory)
    {
        _apiFactory = apiFactory;
        _client = RestService.For<IToDoClient>(apiFactory.CreateClient());
    }

    [Fact]
    public async Task Complete_ReturnsOK_WhenRequestIsValid()
    {
        await _apiFactory.InsertToDoItemForEndpointTesting(_toDoItem);
        IApiResponse response = await _client.ToggleCompleteToDoItem(_toDoItem.Id);
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task Complete_ReturnsBadRequest_WhenRequestIsNotValid()
    {
        IApiResponse response = await _client.ToggleCompleteToDoItem(Guid.Empty);
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task Complete_ReturnsNotFound_WhenEntityDoesNotExists()
    {
        IApiResponse response = await _client.ToggleCompleteToDoItem(Guid.NewGuid());
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}