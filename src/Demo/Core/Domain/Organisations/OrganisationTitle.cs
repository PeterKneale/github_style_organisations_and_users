namespace Demo.Core.Domain.Organisations;

public class OrganisationTitle : BaseValueObject
{
    private OrganisationTitle()
    {
    }

    public OrganisationTitle(string title, string? description = null)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title), "title is null");
        Description = description;
    }

    public string Title { get; private set; }
    public string? Description { get; private set; }
    
    public string FullName => Description == null ? Title : $"{Title} {Description}";

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Title;
        if (Description != null)
        {
            yield return Description;
        }
    }
}