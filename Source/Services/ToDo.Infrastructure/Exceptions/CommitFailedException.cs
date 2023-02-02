using System.Runtime.Serialization;

namespace ToDo.Infrastructure.Exceptions;

[Serializable]
public sealed class CommitFailedException : Exception
{
    public CommitFailedException(string message) : base(message) { }

    private CommitFailedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}