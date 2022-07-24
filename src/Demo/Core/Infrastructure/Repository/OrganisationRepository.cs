using Demo.Core.Application.Contracts;
using Demo.Core.Domain.Organisations;
using Demo.Core.Infrastructure.Persistence;

namespace Demo.Core.Infrastructure.Repository;

public class OrganisationRepository : IOrganisationRepository
{
    private readonly DatabaseContext _db;

    public OrganisationRepository(DatabaseContext db)
    {
        _db = db;
    }
    
    public async Task Add(Organisation organisation)
    {
        await _db.Organisations.AddAsync(organisation);
    }

    public async Task<Organisation?> Get(OrganisationId organisationId)
    {
        return await _db
            .Organisations
            .SingleOrDefaultAsync(x => x.Id.Equals(organisationId));
    }
    public async Task<IEnumerable<Organisation>> List()
    { 
        return await _db
            .Organisations
            .ToListAsync();
    }
}