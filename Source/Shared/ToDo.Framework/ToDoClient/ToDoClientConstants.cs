namespace ToDo.Framework.ToDoClient;

public static class ToDoClientConstants
{
    public const string GetPaginatedTodos = TodoitemsRoutePrefix;
    
    public const string PostTodo = TodoitemsRoutePrefix;

    public const string PatchTodo = TodoitemsRoutePrefix + PatchDeleteSuffix;
    
    public const string DeleteTodo = TodoitemsRoutePrefix + PatchDeleteSuffix;
    
    public const string PutTodo = TodoitemsRoutePrefix + PutSuffix;
    
    private const string TodoitemsRoutePrefix= "/todoItems";

    private const string PutSuffix = "/{request.ToDoItemId}";

    private const string PatchDeleteSuffix = "/{toDoItemId}";
}