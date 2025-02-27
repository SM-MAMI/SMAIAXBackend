namespace SMAIAXBackend.Domain.Model.ValueObjects.Ids;

public class TenantId : ValueObject
{
    public Guid Id { get; }

    // Needed by EF Core
    private TenantId() { }

    public TenantId(Guid id)
    {
        Id = id;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }

    public override string ToString()
    {
        return Id.ToString();
    }
}