using KanopyFunctions.Elements.Films;
using KanopyFunctions.Elements.Persons;
using KanopyFunctions.Elements.Shared;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://www.mongodb.com/developer/languages/csharp/csharp-crud-tutorial/ - документация по работе с БД
            //const string connectionUri = "mongodb+srv://root:root@atlascluster.azkpkbd.mongodb.net/";
            //var mongoClient = new MongoClient(connectionUri);
            //var database = mongoClient.GetDatabase("KanopyDB");
            //var collectionFilms = database.GetCollection<BsonDocument>("Films");
            //var collectionDirectors = database.GetCollection<BsonDocument>("Directors");
            //var collectionActors = database.GetCollection<BsonDocument>("Actors");
            //var collectionProducers = database.GetCollection<BsonDocument>("Producers");
            //var collectionScreenwriters = database.GetCollection<BsonDocument>("Screenwriters");
            //var collectionCountries = database.GetCollection<BsonDocument>("Countries");


            //List<string> genres = new List<string> { "Боевик", "Приключения", "Фантаскика" };
            //List<string> films = new List<string> { "Человек-паук: Нет пути домой" };
            //List<Actor> actors = new List<Actor> { new Actor("Том Холланд", 27, films), new Actor("Зендая", 26, films) };
            //List<Producer> producers = new List<Producer> { new Producer("Кевин Файги", films), new Producer("Эми Паскаль", films) };
            //var director = new Director("Джон Уоттс", films);
            //List<Screenwriter> screenwriters = new List<Screenwriter> { new Screenwriter("Крис МакКенна", films), new Screenwriter("Эрик Соммерс", films) };
            //var film = new Film("Человек-паук: Нет пути домой", 2021, "США", director, genres, actors, producers, screenwriters);

            //var Genres = "";
            //foreach (var genre in genres)
            //    Genres += genre + ", ";
            //var Actors = "";
            //foreach (var actor in actors)
            //    Actors += actor.Name + ", ";
            //var Producers = "";
            //foreach (var producer in producers)
            //    Producers += producer.Name + ", ";
            //var Screenwriters = "";
            //foreach (var screenwriter in screenwriters)
            //    Screenwriters += screenwriter.Name + ", ";

            //var addingFilm = new BsonDocument
            //{
            //    { "film_id", 3 },
            //    { "Название", film.Name },
            //    { "Год производства", film.PremiereDate },
            //    { "Страна", film.Country },
            //    { "Жанр", Genres },
            //    { "Режиссер", film.Director.Name },
            //    { "В ролях", Actors },
            //    { "Сценарист", Screenwriters },
            //    { "Продюсер", Producers }
            //};
            //collectionFilms.InsertOne(addingFilm);

            //var allFilms = collectionFilms.Find(new BsonDocument()).ToList();
            //foreach (var el in allFilms)
            //    Console.WriteLine(el);

            // #watch
            var parser = new FilmParser();
            var links = parser.GetLinks("https://kino.mail.ru/cinema/all/");
            //Console.WriteLine(parser.GetCountry(links[0]));
            //foreach (var link in links)
            //{
            //    Console.WriteLine(parser.GetId(link));
            //}
            //Console.WriteLine(parser.GetTitle(links[0]));
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine($"Фильм {i + 1}:");
                var film = parser.GetFilmFromHtmlData(links[i]);
                Console.WriteLine("Название:" + " " + film.Name);
                Console.WriteLine("Премьера:" + " " + film.PremiereDate);
                Console.WriteLine("Возрастное ограничение:" + " " + film.AgeRestriction);
                Console.WriteLine("Страна:" + " " + string.Join(", ", film.Countries));
                Console.WriteLine("Режиссер (id):" + " " + string.Join(", ", film.DirectorsId));
                Console.WriteLine("Жанры:" + " " + string.Join(", ", film.Genres));
                Console.WriteLine("Актеры (id):" + " " + string.Join(", ", film.ActorsId));
                Console.WriteLine("Продюсеры (id):" + " " + string.Join(", ", film.ProducersId));
                Console.WriteLine("Сценаристы (id):" + " " + string.Join(", ", film.AuthorsId));
                Console.WriteLine($"\n\n\n\n\n");
            }
            //Console.WriteLine(parser.GetHtmlData(links[2]));
            // var actors = parser.GetNamesOfTheRole(links[0], "actor");
            // foreach (var actor in actors)
            // {
            //     Console.WriteLine(actor);
            //}



            var personParser = new PersonParser();
            var personLinks = personParser.GetLinks(links[0], Roles.actor);
            foreach (var link in personLinks)
            {
                Console.WriteLine(link + " " + " " + " " + personParser.GetName(link));
            }
        }
        
    }
}