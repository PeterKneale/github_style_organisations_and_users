namespace Demo.Core.Domain.Organisations;

public class OrganisationId : BaseValueObject
{
    private OrganisationId()
    {
        
    }
    private OrganisationId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Organisation id cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static OrganisationId CreateInstance(Guid value) => new(value);

    public Guid Value { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}