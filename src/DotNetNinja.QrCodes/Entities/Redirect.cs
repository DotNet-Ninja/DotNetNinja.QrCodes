using DotNetNinja.QrCodes.Constants;

namespace DotNetNinja.QrCodes.Entities;

public class Redirect
{
    public string Id { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Uri Url { get; set; } = Default.RedirectUri;
    public bool IsEnabled { get; set; } = false;
}