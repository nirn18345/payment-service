namespace PaymentsService.Application.Exceptions
{
    public class AuthenticationException : BaseCustomException
    {
        public AuthenticationException(string? message, int statusCode = 401)
            : base(message ?? "Authentication Exception", statusCode)
        {
        }
    }
}

