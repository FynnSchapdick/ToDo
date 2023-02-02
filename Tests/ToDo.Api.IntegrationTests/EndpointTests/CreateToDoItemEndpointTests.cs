using System.Net;
using ToDo.Framework.ToDoClient.Contracts;

namespace ToDo.Api.IntegrationTests.EndpointTests;

public sealed class CreateToDoItemEndpointTests : IClassFixture<ToDoApiFactory>
{
    private readonly IToDoClient _client;

    private readonly CreateToDoItemRequest _validRequest = new Faker<CreateToDoItemRequest>()
        .CustomInstantiator(faker => new CreateToDoItemRequest(faker.Random.String2(5, 255)))
        .Generate();
    
    private readonly CreateToDoItemRequest _invalidRequest = new Faker<CreateToDoItemRequest>()
        .CustomInstantiator(faker => new CreateToDoItemRequest(faker.Random.String2(1, 4)))
        .Generate();

    public CreateToDoItemEndpointTests(ToDoApiFactory apiFactory)
    {
        _client = RestService.For<IToDoClient>(apiFactory.CreateClient());
    }

    [Fact]
    public async Task Create_ReturnsCreated_WhenRequestIsValid()
    {
        IApiResponse response = await _client.CreateToDoItem(_validRequest);
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
    
    
    [Fact]
    public async Task Create_ReturnsBadRequest_WhenRequestIsNotValid()
    {
        IApiResponse response = await _client.CreateToDoItem(_invalidRequest);
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}