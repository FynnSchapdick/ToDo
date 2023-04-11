namespace ToDo.Api.Core.Dtos;

public sealed record ToDoItemDto(Guid ToDoItemId, string Text, string Status);