using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using RestSharp;

namespace KanopyFunctions.Elements.Shared
{
    public class Parser
    {
        public string GetHtmlData(string link)
        {
            var client = new RestClient(link);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }
        public IHtmlDocument GetHtmlDocument(string link)
        {
            var content = GetHtmlData(link);
            var parser = new HtmlParser();
            var document = parser.ParseDocument(content);
            return document;
        }
    }
}
