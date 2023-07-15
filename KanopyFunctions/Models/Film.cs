namespace KanopyFunctions.Models
{
    public class Film
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PremiereDate { get; set; }
        public string AgeRestriction { get; set; }
        public List<string> Countries { get; set; }
        public List<string> DirectorsId { get; set; }
        public List<string> ActorsId { get; set; }
        public List<string> ProducersId { get; set; }
        public List<string> AuthorsId { get; set; }
        public List<string> Genres { get; set; }
    }
}
