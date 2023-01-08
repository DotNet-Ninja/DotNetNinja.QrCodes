using DotNetNinja.AutoBoundConfiguration;

namespace DotNetNinja.QrCodes.Configuration;

[AutoBind("Exceptions")]
public class ExceptionSettings
{
    public bool EnableDeveloperExceptionsPage { get; set; }
}