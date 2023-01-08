using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DotNetNinja.QrCodes.TagHelpers;


[HtmlTargetElement(Attributes = "is-development")]
public class IsDevelopmentTagHelper : TagHelper
{
    public IsDevelopmentTagHelper(IWebHostEnvironment environment)
    {
        Environment = environment;
    }

    [HtmlAttributeName("is-development")]
    public bool IsDevelopment { get; set; } = true;

    protected internal IWebHostEnvironment Environment { get; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (IsDevelopment != Environment.IsDevelopment())
        {
            output.Content.Clear();
            output.TagName = null;
        }
        return base.ProcessAsync(context, output);
    }
}