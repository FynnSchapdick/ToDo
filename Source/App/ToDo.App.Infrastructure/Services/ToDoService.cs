using ToDo.App.Core;
using ToDo.App.Infrastructure.Services.Abstractions;
using ToDo.Shared.Contracts.Extensions;
using ToDo.Shared.Contracts.V1.Requests;

namespace ToDo.App.Infrastructure.Services;

public sealed class ToDoService : IToDoService
{
    private readonly IToDoClient _client;

    public ToDoService(IToDoClient client)
    {
        _client = client;
    }
    public async Task<IEnumerable<ToDoItemModel>> GetToDoItems(int page, int pageSize)
        => (await _client.GetPaginatedToDoItems(page, pageSize))
            .Validate().Content!
            .Transform(x => new ToDoItemModel(x.ToDoItemId, x.Text, x.Completed));

    public async Task CreateToDoItems(string text)
        => (await _client.CreateToDoItem(new CreateToDoItemRequest(text)))
            .Validate();

    public async Task DeleteToDoItem(Guid id)
        => (await _client.DeleteToDoItem(id)).Validate();

    public async Task UpdateToDoItemText(Guid id, string text)
        => (await _client.UpdateToDoItem(new UpdateToDoItemRequest(id, new UpdateToDoItemRequestBody(text))))
            .Validate();
}