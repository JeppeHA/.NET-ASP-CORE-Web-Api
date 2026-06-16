public class Round()
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public Guid PlayerId { get; set;}

    public Guid CourseId {get; set;}

    public DateTime RoundDate {get; init;} 


}