namespace KanopyDB.Models
{
    public class Producer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> Films { get; set; }

        public Producer(string name, List<string> films)
        {
            Name = name;
            Films = films;
        }
    }
}
