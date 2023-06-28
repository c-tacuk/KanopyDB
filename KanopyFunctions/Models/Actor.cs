namespace KanopyFunctions.Models
{
    public class Actor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<string> Films { get; set; }
    }
}
