using AngleSharp.Dom;
using KanopyFunctions.Elements.Films;
using KanopyFunctions.Elements.Persons;
using KanopyFunctions.Elements.Shared;
using KanopyFunctions.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://www.mongodb.com/developer/languages/csharp/csharp-crud-tutorial/ - документация по работе с БД
            const string connectionUri = "mongodb+srv://root:root@atlascluster.azkpkbd.mongodb.net/";
            var mongoClient = new MongoClient(connectionUri);
            var database = mongoClient.GetDatabase("KanopyDB");
            var collectionFilms = database.GetCollection<BsonDocument>("Films");

            var films = new List<Film>();
            var parser = new FilmParser();
            var links = parser.GetLinks("https://kino.mail.ru/cinema/all/");
            //for (int i = 1; i < 6; i++)
            //{
            //    var film = parser.GetFilmFromHtmlData(links[i]);
            //    films.Add(film);
            //    var countries = film.Countries.AsEnumerable();
            //    var addingFilm = new BsonDocument
            //{
            //    { "film_id", film.Id },
            //    { "Название", film.Name },
            //    { "Год производства", film.PremiereDate },
            //    { "Страна", new BsonArray
            //        {
            //        new BsonDocument{ {"Страна", string.Join(", ", film.Countries) } }
            //        }
            //    },
            //    { "Жанр", new BsonArray
            //        {
            //        new BsonDocument{ {"Жанр", string.Join(", ", film.Genres) } }
            //        }
            //    },
            //    { "Режиссер", new BsonArray
            //        {
            //        new BsonDocument{ {"Id", string.Join(", ", film.DirectorsId) } }
            //        }
            //    },
            //    { "Актеры", new BsonArray
            //        {
            //        new BsonDocument{ {"Id", string.Join(", ", film.ActorsId) } }
            //        }
            //    },
            //    { "Сценарий", new BsonArray
            //        {
            //        new BsonDocument{ {"Id", string.Join(", ", film.AuthorsId) } }
            //        }
            //    },
            //    { "Продюсеры", new BsonArray
            //        {
            //        new BsonDocument{ {"Id", string.Join(", ", film.ProducersId) } }
            //        }
            //    }
            //};
            //    collectionFilms.InsertOne(addingFilm);
            //    Console.WriteLine(i);
            //}

            for (int i = 1; i < 6; i++)
            {
                var film = parser.GetFilmFromHtmlData(links[i]);
                films.Add(film);
                var countries = film.Countries.AsEnumerable();
                var addingFilm = new BsonDocument
            {
                { "film_id", film.Id },
                { "Название", film.Name },
                { "Год производства", film.PremiereDate },
                { "Страна",  string.Join(", ", film.Countries) },
                { "Жанр", string.Join(", ", film.Genres) },
                { "Режиссер", string.Join(", ", film.DirectorsId) },
                { "Актеры", string.Join(", ", film.ActorsId)
                },
                { "Сценарий", string.Join(", ", film.AuthorsId)
                },
                { "Продюсеры", string.Join(", ", film.ProducersId)
                }
            };
                collectionFilms.InsertOne(addingFilm);
                Console.WriteLine(i);
            }
        }
    }
}