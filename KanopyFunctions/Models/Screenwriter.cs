namespace KanopyDB.Models
{
    public class Screenwriter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> Films { get; set; }

        public Screenwriter(string name, List<string> films)
        {
            Name = name;
            Films = films;
        }
    }
}
