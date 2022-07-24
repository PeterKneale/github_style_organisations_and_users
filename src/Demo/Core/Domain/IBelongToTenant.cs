using Demo.Core.Domain.Organisations;

namespace Demo.Core.Domain;

public interface IBelongToOrganisation
{
    OrganisationId OrganisationId { get; set; }
}