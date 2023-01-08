using System.Web;

namespace DotNetNinja.QrCodes.Extensions;

public static class UriExtensions
{
    public static Uri ReplaceOrAppendQueryParameter(this Uri uri, string name, string value)
    {
        var builder = new UriBuilder(uri);
        var items = HttpUtility.ParseQueryString(builder.Query);
        if (items.AllKeys.Contains(name))
        {
            items[name] = value;
        }
        else
        {
            items.Add(name, value);
        }
        builder.Query = items.ToString();
        return new Uri(builder.ToString(), UriKind.RelativeOrAbsolute);
    }

    public static Uri OverrideGravatarSize(this Uri uri, int size)
    {
        if (uri.Host.Contains("gravatar.com", StringComparison.CurrentCultureIgnoreCase))
        {
            return uri.ReplaceOrAppendQueryParameter("s", size.ToString());
        }
        return uri;
    }
}