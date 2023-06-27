using AngleSharp.Browser;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using RestSharp;
using System.Xml.Linq;

namespace KanopyDB
{
    public class Parser
    {
        string content { get; set; }
        public string GetHtmlData(string link)
        {
            var client = new RestClient(link);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }
        public List<string> GetLinksFromHtml(string link)
        {
            content = GetHtmlData(link);
            List<string> hrefTags = new List<string>();
            var parser = new HtmlParser();
            var document = parser.ParseDocument(content);
            foreach (IElement element in document.QuerySelectorAll("a"))
            {
                var parsedLink = element.GetAttribute("href");
                if (parsedLink != null && element.GetAttribute("href").Contains("/cinema/movies/"))
                    hrefTags.Add("https://kino.mail.ru"+parsedLink);
            }
            return hrefTags;
        }
        public List<string> GetActors(string link)
        {
            // <div class="" itemprop="actor" itemscope="" itemtype="http://schema.org/Person"><a class=""
            // itemprop="url" href="/person/629975_tom_holland/"></a><meta class="" itemprop="name" content="Том Холланд"/></div>
            // <a class="" itemprop="url" href="/person/629975_tom_holland/"></a><meta class="" itemprop="name" content="Том Холланд"/></div>
            content = GetHtmlData(link);
            List<string> hrefTags = new List<string>();
            var parser = new HtmlParser();
            var document = parser.ParseDocument(content);
            foreach (IElement element in document.QuerySelectorAll("div"))
            {
                var itemprop = element.GetAttribute("itemprop");
                if (itemprop == "actor")
                {
                    var name = element.InnerHtml.Substring(element.InnerHtml.IndexOf("content")+9);
                    name = name.Substring(0, name.Length - 2);
                    hrefTags.Add(name);
                }
                    
            }
            return hrefTags;
        }
    }
}
