using AngleSharp.Dom;
using AngleSharp.Html.Parser;

namespace KanopyDB
{
    public class Parser
    {
        public IEnumerable<string> GetLinks(string content)
        {
            List<string> hrefTags = new List<string>();

            var parser = new HtmlParser();
            var document = parser.ParseDocument(content);
            foreach (IElement element in document.QuerySelectorAll("a"))
            {
                var link = element.GetAttribute("href");
                if (link != null && element.GetAttribute("href").Contains("/cinema/movies/"))
                    hrefTags.Add(link);
            }
            return hrefTags;
        }
    }
}
