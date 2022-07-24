namespace Demo.Core.Domain.Organisations;

public class OrganisationMemberId : BaseValueObject
{
    private OrganisationMemberId()
    {
        
    }
    private OrganisationMemberId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Organisation member id cannot be empty", nameof(value));
        }

        Value = value;
    }

    public static OrganisationMemberId CreateInstance(Guid value) => new(value);

    public Guid Value { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}