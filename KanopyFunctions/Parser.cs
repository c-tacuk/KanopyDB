using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using KanopyFunctions.Models;
using RestSharp;


namespace KanopyFunctions
{
    public class Parser
    {
        string content { get; set; } = "";
        List<string> genres = new Genres().genres;
        public string GetHtmlData(string link)
        {
            var client = new RestClient(link);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }
        public Film GetFilmFromHtmlData(string link)
        {
            var film = new Film()
            {
                Name = GetTitle(link),
                PremiereDate = GetPremierDate(link),
                Country = GetCountry(link),
                AgeRestriction = GetAgeRestriction(link),
                Director = GetNamesOfTheRole(link, "director")[0],
                Actors = GetNamesOfTheRole(link, "actor"),
                Producers = GetNamesOfTheRole(link, "producer"),
                Authors = GetNamesOfTheRole(link, "author"),
                Genres = GetGenresOfTheFilm(link)
            };
            return film;
        }
        public List<string> GetLinks(string link)
        {
            content = GetHtmlData(link);
            var hrefTags = new List<string>();
            var parser = new HtmlParser();
            var document = parser.ParseDocument(content);
            foreach (IElement element in document.QuerySelectorAll("a"))
            {
                var parsedLink = element.GetAttribute("href");
                if (parsedLink != null && element.GetAttribute("href").Contains("/cinema/movies/") && !element.GetAttribute("href").Contains("#watch")) // !element.GetAttribute("href").Contains("#watch") для того, чтобы не было повторных ссылок
                    hrefTags.Add("https://kino.mail.ru"+parsedLink);
            }
            return hrefTags;
        }
        public string GetTitle(string link)
        {
            content = GetHtmlData(link);
            var parser = new HtmlParser();
            var document = parser.ParseDocument(content);
            foreach (IElement element in document.QuerySelectorAll("meta"))
            {
                var property = element.GetAttribute("property");
                if (property == "mrc__share_title")
                {
                    var strartInd = element.GetAttribute("content").IndexOf("«") + 1;
                    var endInd = element.GetAttribute("content").IndexOf("»") - 1;
                    return element.GetAttribute("content").Substring(strartInd, endInd);
                }
            }
            return "";
        }//17
        public string GetPremierDate(string link)
        {
            content = GetHtmlData(link);
            var parser = new HtmlParser();
            var document = parser.ParseDocument(content);
            foreach (IElement element in document.QuerySelectorAll("meta"))
            {
                var property = element.GetAttribute("property");
                if (property == "mrc__share_title")
                {
                    var strartInd = element.GetAttribute("content").IndexOf("»") + 2;
                    return element.GetAttribute("content").Substring(strartInd).Replace("(", "").Replace(")", "").Substring(0, 4);
                }
            }
            return "";
        }
        public List<string> GetGenresOfTheFilm(string link)
        {
            content = GetHtmlData(link);
            var filmGenres = new List<string>();
            var parser = new HtmlParser();
            var document = parser.ParseDocument(content);
            foreach (IElement element in document.QuerySelectorAll("span"))
            {
                var cl = element.GetAttribute("class");
                if (cl == "badge__text" && genres.Contains(element.TextContent))
                {
                    filmGenres.Add(element.TextContent);
                }
            }
            return filmGenres;
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
                var href = element.QuerySelector("a");
                if (itemprop == role && href != null)
                { 
                    var name = element.QuerySelector("meta").GetAttribute("content");
                    hrefTags.Add(name);
                }
            }
            return hrefTags;
        }
        public string GetCountry(string link)
        {
            content = GetHtmlData(link);
            var parser = new HtmlParser();
            var document = parser.ParseDocument(content);
            for (int i = 0; i < document.QuerySelectorAll("span").Length-1; i++)
            {
                var element = document.QuerySelectorAll("span")[i]; // у названия страны в html данных нет никаких отличительных классов и атрибутов,
                                                                    // поэтому приходится по span находить текст "Страна" и следующий span - название страны
                var text = element.TextContent;
                if (text == "Страна")
                {
                    return document.QuerySelectorAll("span")[i + 1].TextContent; 
                }
            }            
            return "";
        }
        public string GetAgeRestriction(string link) 
        {
            content = GetHtmlData(link);
            var parser = new HtmlParser();
            var document = parser.ParseDocument(content);
            for (int i = 0; i < document.QuerySelectorAll("span").Length - 1; i++)
            {
                var element = document.QuerySelectorAll("span")[i]; // у возрастного ограничения в html данных нет никаких отличительных классов и атрибутов,
                                                                    // поэтому приходится по span находить текст "Возрастные ограничения" и следующий span - нужные данные
                var text = element.TextContent;
                if (text == "Возрастные ограничения")
                {
                    return document.QuerySelectorAll("span")[i + 1].TextContent;
                }
            }
            return "";
        }
    }
}
