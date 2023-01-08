using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DotNetNinja.QrCodes.TagHelpers;

[HtmlTargetElement("version", TagStructure = TagStructure.WithoutEndTag)]
public class VersionTagHelper: TagHelper
{
    [HtmlAttributeName("visible")] 
    public bool Visible { get; set; } = true;

    [HtmlAttributeName("prefix")]
    public string Prefix { get; set; } = string.Empty;

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var version = typeof(VersionTagHelper).Assembly.GetName().Version?.ToString() ?? "0.0.0.0";
        if (Visible)
        {
            output.TagName = "span";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.SetHtmlContent($"{Prefix} {version}".Trim());
        }
        else
        {
            output.TagName = null;
            output.Content.SetHtmlContent($"<!-- Version: {version} -->");
        }
        return base.ProcessAsync(context, output);
    }
}