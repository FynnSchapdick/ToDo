namespace ToDo.Shared.Constants;

public static class ToDoConstants
{
    public const string GetPaginatedTodos = TodoItemsRoutePrefix;
    
    public const string PostTodo = TodoItemsRoutePrefix;

    public const string PatchTodo = TodoItemsRoutePrefix + PatchDeleteSuffix;
    
    public const string DeleteTodo = TodoItemsRoutePrefix + PatchDeleteSuffix;
    
    public const string PutTodo = TodoItemsRoutePrefix + PutSuffix;

    private const string TodoItemsRoutePrefix= "/todoItems";

    private const string PutSuffix = "/{request.ToDoItemId}";

    private const string PatchDeleteSuffix = "/{toDoItemId}";
}