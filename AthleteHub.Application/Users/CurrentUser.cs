namespace AthleteHub.Application.Users;

public class CurrentUser
{
    public CurrentUser(string name, string id, IEnumerable<string> roles)
    {
        Name = name;
        Id = id;
        Roles = roles;
    }
    public string Name { get; private set; }
    public string Id { get; private set; }
    public IEnumerable<string> Roles { get; private set; }
    public bool IsInRole(string role) => Roles.Contains(role);

}
