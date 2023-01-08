using DotNetNinja.AutoBoundConfiguration;

namespace DotNetNinja.QrCodes.Configuration;

[AutoBind("Site")]
public class SiteSettings
{
    public string CopyrightOwner { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;

    public ThemeDefinition Theme =>
        Themes.SingleOrDefault(theme => theme.Name.Equals(ThemeName, StringComparison.CurrentCultureIgnoreCase)) ??
        Themes[0];

    public List<ThemeDefinition> Themes { get; set; } = new List<ThemeDefinition>();

    public string ThemeName { get; set; } = string.Empty;
}