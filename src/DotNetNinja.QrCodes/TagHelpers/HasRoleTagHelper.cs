using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DotNetNinja.QrCodes.TagHelpers;

[HtmlTargetElement(Attributes = "has-role")]
public class HasRoleTagHelper: TagHelper
{
    public HasRoleTagHelper(IHttpContextAccessor contextAccessor)
    {
        Context = contextAccessor.HttpContext;
    }

    [HtmlAttributeName("has-role")]
    public string HasRole { get; set; } = string.Empty;

    protected internal HttpContext? Context { get; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (!Context?.User.IsInRole(HasRole) ?? false)
        {
            output.Content.Clear();
            output.TagName = null;
        }
        return base.ProcessAsync(context, output);
    }
}