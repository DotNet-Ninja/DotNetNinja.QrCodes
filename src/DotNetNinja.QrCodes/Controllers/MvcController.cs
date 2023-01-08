using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetNinja.QrCodes.Controllers;

[Authorize]
public abstract class MvcController : Controller
{
    protected ILogger Logger { get; }

    protected MvcController(ILogger logger)
    {
        Logger = logger;
    }
}
