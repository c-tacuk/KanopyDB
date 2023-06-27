using Amazon.Auth.AccessControlPolicy;
using AngleSharp.Common;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using KanopyDB;
using KanopyDB.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using RestSharp;
using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Linq;

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



            var parser = new Parser();
            var links = parser.GetLinksFromHtml("https://kino.mail.ru/cinema/all/");
            //foreach (var link in links)
            //{
            //    Console.WriteLine(link);
            //}

            //Console.WriteLine(parser.GetHtmlData(links[0]));
            var actors = parser.GetActors(links[0]);
            foreach (var actor in actors)
            {
                Console.WriteLine(actor);
            }
        }
        
    }
}