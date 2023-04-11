using ToDo.Api.Core.Domain;
using ToDo.Client;

namespace ToDo.Api.IntegrationTests.EndpointTests;

public abstract class BaseToDoItemEndpointTests : IClassFixture<ToDoApiFactory>
{
    protected IToDoClient Client { get; }
    protected ToDoApiFactory Factory { get; }
    protected ToDoItem[] ToDoItems { get; }
    protected string ValidText { get; } = new Faker<string>()
        .CustomInstantiator(faker => faker.Random.String2(5, 255))
        .Generate();
    protected string InvalidText { get; } = new Faker<string>()
        .CustomInstantiator(faker => faker.Random.String2(1, 4))
        .Generate();
    protected BaseToDoItemEndpointTests(ToDoApiFactory apiFactory)
    {
        Factory = apiFactory;
        Client = RestService.For<IToDoClient>(apiFactory.CreateClient());
        ToDoItems = Enumerable.Range(1, 100).Select(_ => new ToDoItem(ValidText)).ToArray();
    }
}