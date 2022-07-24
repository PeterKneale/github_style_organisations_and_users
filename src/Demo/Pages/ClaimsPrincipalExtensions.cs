using System.Security.Claims;
using Demo.Core.Domain.Organisations;
using Demo.Core.Domain.Users;

namespace Demo.Pages;

public static class ClaimsPrincipalExtensions
{
    public static UserId GetUserId(this ClaimsPrincipal principal)
    {
        return UserId.CreateInstance(Guid.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier)));
    }
    
    public static OrganisationId GetOrganisationId(this ClaimsPrincipal principal)
    {
        return OrganisationId.CreateInstance(Guid.Parse(principal.FindFirstValue("Organisation_id")));
    }
    
    public static string GetOrganisationTitle(this ClaimsPrincipal principal)
    {
        return principal.FindFirstValue("Organisation_title");
    }

    public static string GetUserFullName(this ClaimsPrincipal principal)
    {
        return principal.FindFirstValue(ClaimTypes.Name);
    }
    public static string GetUserEmail(this ClaimsPrincipal principal)
    {
        return principal.FindFirstValue(ClaimTypes.Email);
    }

}