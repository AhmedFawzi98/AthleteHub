namespace AthleteHub.Domain.Exceptions;

public class UnAuthorizedException : Exception
{
    public UnAuthorizedException(string message = "User is not authenticated"):base(message)
    {
    }
}
