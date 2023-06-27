using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using RestSharp;


namespace KanopyDB
{
    public class Parser
    {
        string content { get; set; } = "";
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
        public string GetTitle(string link)
        {
            content = GetHtmlData(link);
            List<string> hrefTags = new List<string>();
            var parser = new HtmlParser();
            var document = parser.ParseDocument(content);
            foreach (IElement element in document.QuerySelectorAll("meta"))
            {
                var property = element.GetAttribute("property");
                if (property == "mrc__share_title")
                {
                    return element.GetAttribute("content").Replace("«", "").Replace("»", "");
                }
            }
            return "";
        }
        public List<string> GetNamesOfTheRole(string link, string role) //actor, director, producer, author
        {
            content = GetHtmlData(link);
            List<string> hrefTags = new List<string>();
            var parser = new HtmlParser();
            var document = parser.ParseDocument(content);
            foreach (IElement element in document.QuerySelectorAll("div"))
            {
                var itemprop = element.GetAttribute("itemprop");
                if (itemprop == role)
                {
                    var name = GetNameFromHtmlString(element.InnerHtml);
                    hrefTags.Add(name);
                }
            }
            return hrefTags;
        }
        public string GetCountry(string link) 
        {
            content = GetHtmlData(link);
            List<string> hrefTags = new List<string>();
            var parser = new HtmlParser();
            var document = parser.ParseDocument(content);
            foreach (IElement element in document.QuerySelectorAll("a"))
            {
                var href = element.GetAttribute("href");
                if (href != null && href.Contains("/cinema/all"))
                {
                    Console.WriteLine(element.TextContent);
                }
                    
            }
            return "";
        }
        public string GetNameFromHtmlString(string htmlString)
        {
            var name = htmlString.Substring(htmlString.IndexOf("content") + 9);
            name = name.Substring(0, name.Length - 2);
            return name;
        }
    }
}
