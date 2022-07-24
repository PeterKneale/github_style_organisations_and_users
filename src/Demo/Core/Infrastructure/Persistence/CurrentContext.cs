using System.Security.Claims;
using Demo.Core.Application.Contracts;
using Demo.Core.Domain.Organisations;
using Demo.Core.Domain.Users;

namespace Demo.Core.Infrastructure.Persistence;

public class CurrentContext : ICurrentContext
{
    private readonly IHttpContextAccessor _accessor;
    
    public CurrentContext(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }
    
    public UserId CurrentUserId
    {
        get
        {
            var context = _accessor.HttpContext;
            if (context == null)
            {
                throw new Exception("HttpContext is null");
            }
            var identity = context.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                throw new Exception("ClaimsIdentity is null");
            }

            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                throw new Exception($"{ClaimTypes.NameIdentifier} is not found in claims");
            }

            var id = Guid.Parse(claim.Value);
            return UserId.CreateInstance(id);
        }
    }
}