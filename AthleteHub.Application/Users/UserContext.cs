﻿using System.Security.Claims;
using AthleteHub.Application.Users;
using Microsoft.AspNetCore.Http;

namespace AthleteHub.Application.Users;
public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _contextAccessor;

    public UserContext(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }
    public CurrentUser GetCurrentUser()
    {
        ClaimsPrincipal user = _contextAccessor?.HttpContext?.User;

        if (user == null)
        {
            throw new InvalidOperationException("User context is null");
        }
        if (user.Identity is null || !user.Identity.IsAuthenticated)
        {
            return null;
        }

        var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var userName = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        var userEmail = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var userRoles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(r => r.Value).ToList();

        return new CurrentUser(userName, userId, userEmail, userRoles);
    }
}
