using Demo.Core.Domain.Users;

namespace Demo.Core.Application.Contracts;

public interface IAuthRepository
{
    Task<User?> GetUserByEmail(string email);
}