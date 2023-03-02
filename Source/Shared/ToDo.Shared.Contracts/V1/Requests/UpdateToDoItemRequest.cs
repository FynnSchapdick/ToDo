using Refit;

namespace ToDo.Shared.Contracts.V1.Requests;

public sealed record UpdateToDoItemRequest(Guid ToDoItemId, [Body] UpdateToDoItemRequestBody Body);