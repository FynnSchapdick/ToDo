namespace ToDo.Api.Data.Exceptions;

[Serializable]
public sealed class CommitFailedException : Exception
{
    public CommitFailedException(string message) : base(message) { }
}