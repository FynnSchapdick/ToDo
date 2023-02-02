using System.Runtime.Serialization;

namespace ToDo.Infrastructure.Exceptions;

[Serializable]
public sealed class ToDoItemNotFoundException : Exception
{
    public ToDoItemNotFoundException(string message) : base(message) { }

    private ToDoItemNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}