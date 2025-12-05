namespace PaymentsService.Application.Exceptions
{
    public class RequestException : BaseCustomException
    {
        public RequestException(string? message, int statusCode = 400)
            : base(message ?? "Request Exception", statusCode)
        {
        }
    }
}

