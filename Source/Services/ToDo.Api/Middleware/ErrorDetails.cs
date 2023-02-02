namespace ToDo.Api.Middleware;

public sealed class ErrorDetails
{
    private const string ErrordetailResponse = "Responded with StatusCode: {0} with Message: {1}";
    public int StatusCode { get; }
    public string Message { get; }

    public ErrorDetails(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }

    public override string ToString()
        => string.Format(ErrordetailResponse, StatusCode, Message);
}