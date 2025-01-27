namespace SMAIAXBackend.Domain.Model.ValueObjects.Ids;

public class UserId(Guid id) : ValueObject
{
    public Guid Id { get; private set; } = id;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }

    public override string ToString()
    {
        return Id.ToString();
    }
}