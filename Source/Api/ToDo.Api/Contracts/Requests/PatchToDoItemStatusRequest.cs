namespace ToDo.Api.Contracts.Requests;

public sealed record PatchToDoItemStatusRequest(Guid ToDoItemId);