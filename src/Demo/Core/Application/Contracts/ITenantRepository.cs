using Demo.Core.Domain.Organisations;
using Demo.Core.Domain.Users;

namespace Demo.Core.Application.Contracts;

public interface IOrganisationRepository
{
    Task Add(Organisation organisation);
    Task<Organisation?> Get(OrganisationId organisationId);
    Task<IEnumerable<Organisation>> List();
}