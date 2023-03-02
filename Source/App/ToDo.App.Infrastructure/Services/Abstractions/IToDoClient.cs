using Refit;
using ToDo.Shared.Constants;
using ToDo.Shared.Contracts.V1.Requests;
using ToDo.Shared.Contracts.V1.Responses;

namespace ToDo.App.Infrastructure.Services.Abstractions;

public interface IToDoClient
{
    [Get(ToDoConstants.GetPaginatedTodos)]
    Task<ApiResponse<GetPaginatedToDosResponse>> GetPaginatedToDoItems(int page, int pageSize, string sorts ="", string filters = "");

    [Post(ToDoConstants.PostTodo)]
    Task<IApiResponse> CreateToDoItem([Body] CreateToDoItemRequest request);

    [Patch(ToDoConstants.PatchTodo)]
    Task<IApiResponse> ToggleCompleteToDoItem(Guid toDoItemId);
    
    [Delete(ToDoConstants.DeleteTodo)]
    Task<IApiResponse> DeleteToDoItem(Guid toDoItemId);

    [Put(ToDoConstants.PutTodo)]
    Task<IApiResponse> UpdateToDoItem(UpdateToDoItemRequest request);
}