using System.Security.Claims;
using DotNetNinja.QrCodes.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DotNetNinja.QrCodes.TagHelpers;

public class AvatarTagHelper: TagHelper
{
    private const string DefaultAvatarUrl = "https://www.gravatar.com/avatar/?d=mp";

    public AvatarTagHelper(IHttpContextAccessor context)
    {
        Principal = context.HttpContext?.User;
    }

    protected ClaimsPrincipal? Principal { get; }

    [HtmlAttributeName("size")] 
    public int Size { get; set; } = 32;

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var url = DefaultAvatarUrl;
        var name = "Anonymous";
        if (Principal != null && (Principal.Identity?.IsAuthenticated ?? false))
        {
            url = Principal.Claims.FirstOrDefault(c => c.Type == "picture")?.Value ?? DefaultAvatarUrl;
            name = Principal.Identity.Name;
        }
        var uri = new Uri(url);
        output.TagName = "img";
        output.Attributes.Add("alt", name);
        output.Attributes.Add("title", name);
        output.Attributes.Add("src", uri.OverrideGravatarSize(Size));
        return base.ProcessAsync(context, output);
    }
}