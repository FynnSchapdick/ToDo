using Refit;
using ToDo.Client.Contracts.Requests;
using ToDo.Client.Contracts.Responses;

namespace ToDo.Client;

public interface IToDoClient
{
    [Post("/todoitems")]
    Task<ApiResponse<Guid>> CreateToDoItem([Body] PostToDoItemRequestBody body);

    [Delete("/todoitems/{toDoItemId}")]
    Task<IApiResponse> DeleteToDoItem(Guid toDoItemId);

    [Patch("/todoitems/{toDoItemId}/text")]
    Task<IApiResponse> PatchToDoItemText(Guid toDoItemId, [Body] PatchToDoItemTextRequestBody body);
    
    [Patch("/todoitems/{toDoItemId}/status")]
    Task<IApiResponse> PatchToDoItemStatus(Guid toDoItemId, [Body] PatchToDoItemStatusRequestBody body);
    
    [Get("/todoitems")]
    Task<ApiResponse<GetToDoItemsResponse>> GetToDoItems();
}