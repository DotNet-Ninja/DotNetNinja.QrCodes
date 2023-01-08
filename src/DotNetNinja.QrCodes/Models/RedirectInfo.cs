using DotNetNinja.QrCodes.Constants;

namespace DotNetNinja.QrCodes.Models;

public class RedirectInfo
{
    public string Id { get; set; } = Default.RedirectName;
    public Uri Url { get; set; } = Default.RedirectUri;
}