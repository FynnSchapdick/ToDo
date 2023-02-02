namespace ToDo.Framework.ToDoClient;

public static class ToDoClientConstants
{
    public const string GetPaginatedTodos = TodoitemsRoutePrefix;
    
    public const string PostTodo = TodoitemsRoutePrefix;

    public const string PatchTodo = TodoitemsRoutePrefix + PatchDeletePutSuffix;
    
    public const string DeleteTodo = TodoitemsRoutePrefix + PatchDeletePutSuffix;
    
    public const string PutTodo = TodoitemsRoutePrefix + PatchDeletePutSuffix;
    
    private const string TodoitemsRoutePrefix= "/todoItems";

    private const string PatchDeletePutSuffix = "/{toDoItemId}";
}