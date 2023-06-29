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
                Countries = GetCountries(link),
                AgeRestriction = GetAgeRestriction(link),
                Directors = GetNamesOfTheRole(link, "director"),
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
            foreach (IElement element in document.QuerySelectorAll("div"))
            {
                var cl = element.GetAttribute("class");
                if (cl == "hidden")
                {
                    foreach (IElement metaEl in element.QuerySelectorAll("meta"))
                    {
                        var itemprop = metaEl.GetAttribute("itemprop");
                        if (itemprop == "name")
                        {
                            return metaEl.GetAttribute("content");
                        }
                    }
                }
            }
            return "";
        }
        public string GetPremierDate(string link)
        {
            content = GetHtmlData(link);
            var parser = new HtmlParser();
            var document = parser.ParseDocument(content);
            foreach (IElement element in document.QuerySelectorAll("div"))
            {
                var cl = element.GetAttribute("class");
                if (cl == "margin_bottom_20")
                {
                    var span = element.QuerySelector("span");
                    return span.QuerySelector("a").TextContent;
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
            foreach (IElement element in document.QuerySelectorAll("div"))
            {
                var cl = element.GetAttribute("class");
                if (cl == "hidden")
                {
                    foreach (IElement metaEl in element.QuerySelectorAll("meta"))
                    {
                        var itemprop = metaEl.GetAttribute("itemprop");
                        if (itemprop == "genre")
                        {
                            filmGenres = metaEl.GetAttribute("content").Split(',').ToList();
                            return filmGenres;
                        }
                    }
                }
            }
            return filmGenres;
        }
        public List<string> GetNamesOfTheRole(string link, string role) //actor, director, producer, author
        {
            content = GetHtmlData(link);
            List<string> persons = new List<string>();
            var parser = new HtmlParser();
            var document = parser.ParseDocument(content);
            foreach (IElement element in document.QuerySelectorAll("div"))
            {
                var cl = element.GetAttribute("class");
                if (cl == "hidden")
                {
                    foreach (IElement secondaryEl in element.QuerySelectorAll("div"))
                    {
                        var itemprop = secondaryEl.GetAttribute("itemprop");
                        if (itemprop == role)
                        {
                            persons.Add(secondaryEl.QuerySelector("meta").GetAttribute("content"));
                        }
                    }
                    return persons;
                }
            }
            return persons;
        }
        public List<string> GetCountries(string link)
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
                    return document.QuerySelectorAll("span")[i + 1].TextContent.Split(',').ToList(); 
                }
            }            
            return null;
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
