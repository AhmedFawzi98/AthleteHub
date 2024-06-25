namespace AthleteHub.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string resourceType, string resourceId):base($"{resourceType} [{resourceId}] doesn't exist")
    {
    }
}
