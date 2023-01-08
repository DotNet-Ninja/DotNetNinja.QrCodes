using DotNetNinja.AutoBoundConfiguration;

namespace DotNetNinja.QrCodes.Configuration;

[AutoBind("RedirectCache")]
public class RedirectCacheSettings
{
    public string CacheKey { get; set; } = "redirects";
    public int CacheTimeout { get; set; } = 300;

    public TimeSpan CacheExpiration => TimeSpan.FromSeconds(CacheTimeout);
}