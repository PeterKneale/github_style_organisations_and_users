using Demo.Core.Application.Contracts;
using Demo.Core.Domain.Users;
using Demo.Core.Infrastructure.Persistence;

namespace Demo.Core.Infrastructure.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly DatabaseContext _db;

    public AuthRepository(DatabaseContext db)
    {
        _db = db;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _db
            .Users
            .IgnoreQueryFilters()
            .SingleOrDefaultAsync(x => x.Email == email);
    }
}