using System.Runtime.Serialization;

namespace ToDo.Core.Domain.Exceptions;

[Serializable]
public sealed class ToDoItemException : Exception
{
    public ToDoItemException(string message) : base(message) { }

    public ToDoItemException(string method, object value) : base($"Violated rules in '{method}' with value '{value}'.")
    {
    }

    private ToDoItemException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}