using Refit;

namespace ToDo.Framework.ToDoClient.Contracts;

public sealed record UpdateToDoItemRequest(Guid ToDoItemId, [Body] UpdateToDoItemRequestBody Body);