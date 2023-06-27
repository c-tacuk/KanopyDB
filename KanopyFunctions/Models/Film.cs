namespace KanopyDB.Models
{
    public class Film
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int PremiereDate { get; set; }
        public string Country { get; set; }
        public Director Director { get; set; }
        public List<string> Genres { get; set; }
        public List<Actor> Actors { get; set; }
        public List<Producer> Producers { get; set; }
        public List<Author> Screenwriters { get; set; }

        public Film(string name, int premiereDate, string country, Director director, List<string> genres,
            List<Actor> actors, List<Producer> producers, List<Author> screenwriters) 
        {
            Name = name;
            PremiereDate = premiereDate;
            Country = country;
            Director = director;
            Genres = genres;
            Actors = actors;
            Producers = producers;
            Screenwriters = screenwriters;
        }
    }
}
