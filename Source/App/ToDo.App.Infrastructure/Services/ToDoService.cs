using Refit;
using ToDo.App.Core.Abstractions;
using ToDo.App.Core.Domain;
using ToDo.App.Infrastructure.Extensions;
using ToDo.Client;

namespace ToDo.App.Infrastructure.Services;

public sealed class ToDoService : IToDoService
{
    private readonly IToDoClient _client;

    public ToDoService(IToDoClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<ToDoItem>> GetToDoItems(int page, int pageSize)
        => (await _client.GetPaginatedToDoItems(page, pageSize)).Transform(x => new ToDoItem(x.ToDoItemId, x.Text, x.Status));

    public async Task<Guid> CreateToDoItem(string text)
    {
        ApiResponse<Guid> response = await _client.PostToDoItem(new(text));
        return response.ValidateResponseStruct();
    }

    public async Task<bool> DeleteToDoItem(Guid id)
    {
        IApiResponse response = await _client.DeleteToDoItem(id);
        return response.Validate();
    }

    public async Task<bool> PatchToDoItemText(Guid id, string text)
    {
        IApiResponse response = await _client.PatchToDoItemText(id, new(text));
        return response.Validate();
    }

    public async Task<bool> PatchToDoItemStatus(Guid id, string status)
    {
        IApiResponse response = await _client.PatchToDoItemStatus(id, new(status));
        return response.Validate();
    }
}