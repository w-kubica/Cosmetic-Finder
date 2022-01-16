using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace Cosmetic_Finder.Common.Application.UtilsHtml
{
    public static class HtmlUtils
    {
        public static string ConvertHtmlToString(HtmlDocument html)
        {
            var nodes = html.DocumentNode.InnerText;
            var noHtml = Regex.Replace(nodes, @"<[^>]+>|&nbsp|&lt|&reg;", "").Trim();

            return noHtml;
        }

        public static void RemoveStyles(HtmlDocument html)
        {
            html.DocumentNode.Descendants()
                .Where(n => n.Name is "script" or "style")
                .ToList()
                .ForEach(n => n.Remove());
        }
    }
}
