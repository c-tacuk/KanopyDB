namespace KanopyFunctions.Models
{
    public class Film
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PremiereDate { get; set; }
        public string Country { get; set; }
        public string AgeRestriction { get; set; }
        public string Director { get; set; }
        public List<string> Actors { get; set; }
        public List<string> Producers { get; set; }
        public List<string> Authors { get; set; }
        public List<string> Genres { get; set; }
    }
}
