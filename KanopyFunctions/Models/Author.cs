namespace KanopyDB.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> Films { get; set; }

        public Author(string name, List<string> films)
        {
            Name = name;
            Films = films;
        }
    }
}
