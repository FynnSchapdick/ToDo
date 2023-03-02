namespace ToDo.Shared.Contracts.V1.Responses;

public sealed record ToDoItemDto(Guid ToDoItemId, string Text, bool Completed);