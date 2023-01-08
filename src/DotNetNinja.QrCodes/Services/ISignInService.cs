using Microsoft.AspNetCore.Authentication;

namespace DotNetNinja.QrCodes.Services;

public interface ISignInService
{
    Task ChallengeAsync(string scheme, AuthenticationProperties properties);
    Task SignOutAsync(string scheme, AuthenticationProperties properties);
    Task SignOutAsync(string scheme);
}