using Refit;
using ToDo.Framework.ToDoClient.Contracts;

namespace ToDo.Framework.ToDoClient.Abstractions;

public interface IToDoClient
{
    [Get(ToDoClientConstants.GetPaginatedTodos)]
    Task<ApiResponse<GetPaginatedToDosResponse>> GetPaginatedToDoItems(int page, int pageSize, string sorts ="", string filters = "");

    [Post(ToDoClientConstants.PostTodo)]
    Task<IApiResponse> CreateToDoItem([Body] CreateToDoItemRequest request);

    [Patch(ToDoClientConstants.PatchTodo)]
    Task<IApiResponse> ToggleCompleteToDoItem(Guid toDoItemId);
    
    [Delete(ToDoClientConstants.DeleteTodo)]
    Task<IApiResponse> DeleteToDoItem(Guid toDoItemId);

    [Put(ToDoClientConstants.PutTodo)]
    Task<IApiResponse> UpdateToDoItem(UpdateToDoItemRequest request);
}