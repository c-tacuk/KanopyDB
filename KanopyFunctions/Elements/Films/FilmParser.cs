using AngleSharp.Dom;
using KanopyFunctions.Elements.Persons;
using KanopyFunctions.Elements.Shared;
using KanopyFunctions.Models;


namespace KanopyFunctions.Elements.Films
{
    public class FilmParser
    {
        Parser sharedParser = new Parser();
        PersonParser personParser = new PersonParser();
        public string GetHtmlData(string link)
        {
            return sharedParser.GetHtmlData(link);
        }
        public Film GetFilmFromHtmlData(string filmLink)
        {
            var film = new Film()
            {
                Name = GetTitle(filmLink),
                PremiereDate = GetPremierDate(filmLink),
                Countries = GetCountries(filmLink),
                AgeRestriction = GetAgeRestriction(filmLink),
                DirectorsId = personParser.GetAllId(filmLink, Roles.director),
                ActorsId = personParser.GetAllId(filmLink, Roles.actor),
                ProducersId = personParser.GetAllId(filmLink, Roles.producer),
                AuthorsId = personParser.GetAllId(filmLink, Roles.author),
                Genres = GetGenresOfTheFilm(filmLink)
            };
            return film;
        }
        public List<string> GetLinks(string catalogLink) 
        {
            var movieLinks = new List<string>();
            var document = sharedParser.GetHtmlDocument(catalogLink);
            foreach (IElement element in document.QuerySelectorAll("a"))
            {
                var parsedLink = element.GetAttribute("href");
                if (parsedLink != null && element.GetAttribute("href").Contains("/cinema/movies/") && !element.GetAttribute("href").Contains("#watch")) // !element.GetAttribute("href").Contains("#watch") для того, чтобы не было повторных ссылок
                    movieLinks.Add("https://kino.mail.ru" + parsedLink);
            }
            return movieLinks;
        }
        public string GetTitle(string filmLink)
        {
            var document = sharedParser.GetHtmlDocument(filmLink);
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
        public string GetPremierDate(string filmLink)
        {
            var document = sharedParser.GetHtmlDocument(filmLink);
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
        public List<string> GetGenresOfTheFilm(string filmLink)
        {
            var filmGenres = new List<string>();
            var document = sharedParser.GetHtmlDocument(filmLink);
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
        public List<string> GetNamesOfTheRole(string filmLink, Roles role)
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
                            persons.Add(secondaryEl.QuerySelector("meta").GetAttribute("content"));
                        }
                    }
                    return persons;
                }
            }
            return persons;
        }
        public List<string> GetCountries(string filmLink)
        {
            var document = sharedParser.GetHtmlDocument(filmLink);
            for (int i = 0; i < document.QuerySelectorAll("span").Length - 1; i++)
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
        public string GetAgeRestriction(string filmLink)
        {
            var document = sharedParser.GetHtmlDocument(filmLink);
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
