namespace AthleteHub.Application.Users;

public interface IUserContext
{
    CurrentUser GetCurrentUser();
}