using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DotNetNinja.QrCodes.TagHelpers;

[HtmlTargetElement(Attributes = "is-authenticated")]
public class IsAuthenticatedTagHelper: TagHelper
{
    public IsAuthenticatedTagHelper(IHttpContextAccessor contextAccessor, ILogger<IsAuthenticatedTagHelper> logger)
    {
        Context = contextAccessor.HttpContext;
        Logger = logger;
    }

    [HtmlAttributeName("is-authenticated")]
    public bool IsAuthenticated { get; set; }=true;
    
    protected internal ILogger Logger { get; }
    protected internal HttpContext? Context { get; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (IsAuthenticated != (Context?.User?.Identity?.IsAuthenticated ?? false))
        {
            Logger.LogWarning("Clearing Tag");
            output.Content.Clear();
            output.TagName = null;
        }
        return base.ProcessAsync(context, output);
    }
}