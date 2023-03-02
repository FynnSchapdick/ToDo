using System.Runtime.Serialization;

namespace ToDo.Api.Data.Exceptions;

[Serializable]
public sealed class CommitFailedException : Exception
{
    public CommitFailedException(string message) : base(message) { }

    private CommitFailedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}