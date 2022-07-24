using Demo.Core.Domain.Users;

namespace Demo.Core.Application.Contracts;

public interface ICurrentContext
{
    UserId CurrentUserId { get; }
}