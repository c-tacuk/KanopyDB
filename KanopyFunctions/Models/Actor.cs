namespace KanopyDB.Models
{
    public class Actor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<string> Films { get; set; }

        public Actor(string name, int age, List<string> films) 
        {
            Name = name;
            Age = age;
            Films = films;
        }
    }
}
