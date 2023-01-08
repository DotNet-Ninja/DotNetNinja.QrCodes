using System.Security.Claims;
using DotNetNinja.QrCodes.Constants;

namespace DotNetNinja.QrCodes.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetName(this ClaimsPrincipal principal)
    {
        return principal.GetName(string.Empty);
    }

    public static string GetName(this ClaimsPrincipal principal, string defaultValue)
    {
        return principal.Identity?.Name ?? defaultValue;
    }

    public static IEnumerable<string> GetRoles(this ClaimsPrincipal principal)
    {
        return principal.Claims.Where(c => c.Type == ClaimTypes.Role)?.Select(c => c.Value) ?? new List<string>();
    }

    public static string GetPicture(this ClaimsPrincipal principal)
    {
        return principal.GetPicture(string.Empty);
    }

    public static string GetPicture(this ClaimsPrincipal principal, string defaultValue)
    {
        return ReadFirstValue(principal, CustomClaimsType.Picture, defaultValue);
    }
    
    public static string GetIdentifier(this ClaimsPrincipal principal)
    {
        return principal.GetIdentifier(string.Empty);
    }

    public static string GetIdentifier(this ClaimsPrincipal principal, string defaultValue)
    {
        return ReadFirstValue(principal, ClaimTypes.NameIdentifier, defaultValue);
    }

    public static string GetEmailAddress(this ClaimsPrincipal principal)
    {
        return principal.GetEmailAddress(string.Empty);
    }

    public static string GetEmailAddress(this ClaimsPrincipal principal, string defaultValue)
    {
        return ReadFirstValue(principal, ClaimTypes.Email, defaultValue);
    }

    public static bool IsEmailVerified(this ClaimsPrincipal principal)
    {
        return principal.IsEmailVerified(false);
    }
    
    public static bool IsEmailVerified(this ClaimsPrincipal principal, bool defaultValue)
    {
        var value = ReadFirstValue(principal, CustomClaimsType.EmailVerified, defaultValue.ToString());
        return Convert.ToBoolean(value);
    }

    private static string ReadFirstValue(ClaimsPrincipal principal, string claimType, string defaultValue)
    {
        return principal.Claims.FirstOrDefault(c => c.Type == claimType)?.Value ?? defaultValue;
    }
}