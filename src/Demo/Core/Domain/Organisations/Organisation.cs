using Demo.Core.Domain.Users;

namespace Demo.Core.Domain.Organisations;

public class Organisation : BaseEntity
{
    public OrganisationId Id { get; private init; }

    public OrganisationTitle Title { get; private set; }
    
    private List<OrganisationMember> _members;

    private Organisation()
    {
    }

    public Organisation(OrganisationId id, OrganisationTitle title, UserId createdBy)
    {
        Id = id;
        Title = title;
        _members = new List<OrganisationMember> {new(Id, createdBy, OrganisationMemberRole.Owner)};
    }

    public void ChangeName(OrganisationTitle title)
    {
        Title = title;
        AddDomainEvent(new OrganisationNameChangedEvent(this));
    }

    public static Organisation CreateInstance(OrganisationId id, OrganisationTitle title, UserId createdBy) => new(id, title, createdBy);
}