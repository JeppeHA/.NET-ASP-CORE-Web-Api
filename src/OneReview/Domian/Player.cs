namespace OneReview.Domain;

public class Player
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; init; }

    public int Age { get; init; }

    public string Gender { get; init; }

}