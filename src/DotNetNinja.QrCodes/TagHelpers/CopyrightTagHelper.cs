using DotNetNinja.QrCodes.Configuration;
using DotNetNinja.QrCodes.Services;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DotNetNinja.QrCodes.TagHelpers;

[HtmlTargetElement("copyright", TagStructure = TagStructure.WithoutEndTag)]
public class CopyrightTagHelper: TagHelper
{
    private readonly ITimeProvider _time;
    private readonly SiteSettings _settings;

    public CopyrightTagHelper(ITimeProvider time, SiteSettings settings)
    {
        _time = time;
        _settings = settings;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var currentYear = _time.RequestTime.Year;
        var year = (currentYear == 2023) ? "2023" : $"2023-{currentYear}";
        output.TagName = null;
        output.Content.SetHtmlContent($"&copy; {year} {_settings.CopyrightOwner} - All rights reserved.");
        base.Process(context, output);
    }
}