namespace Demo.Core.Domain.Organisations;

public class OrganisationMemberRole : BaseValueObject
{
    public static OrganisationMemberRole Owner => new OrganisationMemberRole("Owner");

    public static OrganisationMemberRole Member => new OrganisationMemberRole("Member");

    public string Value { get; }

    private OrganisationMemberRole(string value)
    {
        this.Value = value;
    }

    public static OrganisationMemberRole Of(string roleCode)
    {
        return new OrganisationMemberRole(roleCode);
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}