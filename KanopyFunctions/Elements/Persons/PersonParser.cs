using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using KanopyFunctions.Elements.Shared;
using RestSharp;
using System.Data;


namespace KanopyFunctions.Elements.Persons
{
    public class PersonParser
    {
        Parser sharedParser = new Parser();
        public string GetHtmlData(string link)
        {
            return sharedParser.GetHtmlData(link);
        }
        public List<string> GetLinks(string filmLink, Roles role)
        {
            List<string> persons = new List<string>();
            var document = sharedParser.GetHtmlDocument(filmLink);
            foreach (IElement element in document.QuerySelectorAll("div"))
            {
                var cl = element.GetAttribute("class");
                if (cl == "hidden")
                {
                    foreach (IElement secondaryEl in element.QuerySelectorAll("div"))
                    {
                        var itemprop = secondaryEl.GetAttribute("itemprop");
                        if (itemprop == role.ToString())
                        {
                            var parsedLink = secondaryEl.QuerySelector("a").GetAttribute("href");
                            persons.Add("https://kino.mail.ru" + parsedLink);
                        }
                    }
                    return persons;
                }
            }
            return persons;
        }
        public string GetName(string personLink)
        {
            List<string> persons = new List<string>();
            var document = sharedParser.GetHtmlDocument(personLink);
            foreach (IElement element in document.QuerySelectorAll("h1"))
            {
                var cl = element.GetAttribute("class");
                if (cl == "p-person-about__name")
                {
                    return element.TextContent;
                }
            }
            return "";
        }
    }
}
