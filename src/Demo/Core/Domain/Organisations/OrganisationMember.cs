using Demo.Core.Domain.Users;

namespace Demo.Core.Domain.Organisations;

public class OrganisationMember : BaseEntity
{
    private OrganisationMember()
    {
        
    }
    public OrganisationMember(OrganisationId organisationId, UserId userId, OrganisationMemberRole role)
    {
        Id = OrganisationMemberId.CreateInstance(Guid.NewGuid());
        OrganisationId = organisationId;
        UserId = userId;
        Role = role;
    }
    public OrganisationMemberId Id { get; private init; }
    public OrganisationId OrganisationId { get; private init; }
    public UserId UserId { get; private init; }
    public OrganisationMemberRole Role { get; private init; }
}