using Demo.Core.Application.Contracts;
using Demo.Core.Domain.Organisations;
using Demo.Core.Domain.Users;

namespace Demo.IntegrationTests;

public class FakeCurrentContext : ICurrentContext
{
    private static OrganisationId? _organisationId;
    private static UserId? _userId;

    public OrganisationId? CurrentOrganisationId => _organisationId;

    public UserId CurrentUserId => _userId ?? throw new Exception();

    public static void SetUserId(UserId userId) => 
        _userId = userId;
    public static void SetOrganisationId(OrganisationId organisationId) =>
        _organisationId = organisationId;
    
    public static void SetUserId(Guid userId) => 
        SetUserId(UserId.CreateInstance(userId));
    
    public static void SetOrganisationId(Guid organisationId) =>
        SetOrganisationId(OrganisationId.CreateInstance(organisationId));

}