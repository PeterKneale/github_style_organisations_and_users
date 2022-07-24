using Demo.Core.Application.Contracts;
using Demo.Core.Domain.Users;
using FluentValidation;

namespace Demo.Core.Application.Users.Commands;

public static class Register
{
    public record Command(Guid UserId, string FirstName, string LastName, string Email, string Password) : IRequest;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.UserId).NotEmpty();
            RuleFor(m => m.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(m => m.LastName).NotEmpty().MaximumLength(50);
            RuleFor(m => m.Email).NotEmpty().EmailAddress();
            RuleFor(m => m.Password).NotEmpty().MaximumLength(50);
        }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly IUserRepository _users;

        public Handler(IUserRepository users)
        {
            _users = users;
        }

        public async Task<Unit> Handle(Command command, CancellationToken token)
        {
            var userid = UserId.CreateInstance(command.UserId);
            var userName = new UserName(command.FirstName, command.LastName);
            var userEmail = command.Email;
            var userPassword = command.Password;
            
            var user = User.CreateInstance(userid, userName, userEmail, userPassword);

            await _users.Add(user);

            return Unit.Value;
        }
    }
}