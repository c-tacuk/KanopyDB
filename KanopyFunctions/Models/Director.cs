namespace KanopyDB.Models
{
    public class Director
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> Films { get; set; }

        public Director(string name, List<string> films)
        {
            Name = name;
            Films = films;
        }
    }
}
