using DotNetNinja.AutoBoundConfiguration;

namespace DotNetNinja.QrCodes.Configuration;

[AutoBind("DbSettings")]
public class DbSettings
{
    public Dictionary<string, DatabaseConfiguration> Contexts { get; set; } = new();
}