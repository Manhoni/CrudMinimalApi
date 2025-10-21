namespace Person.Models
{
    public class Persons
    {
        public Guid Id { get; init; }
        public string Name { get; private set; } = string.Empty;

        public Persons(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public void ChangeName(string name)
        {
            Name = name;
        }

        public void SetInactive()
        {
            Name = "[DESACTIVATED]";
        }
    }
}
