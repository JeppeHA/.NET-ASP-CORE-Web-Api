public class Course
{
    public Guid Id { get; } = Guid.NewGuid();

    public string Name { get; set;}
    public int NumberOfHoles { get; set; }

    public string Difficulty { get; set; }
}