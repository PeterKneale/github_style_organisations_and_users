using Demo.Core.Application.Contracts;
using Demo.Core.Domain.Users;
using FluentValidation;

namespace Demo.Core.Application.Users.Commands;

public static class Authenticate
{
    public record Command(string Email, string Password) : IRequest<Result>;
    
    public class Result
    {
        public Result(User user)
        {
            User = user;
            Success = true;
        }
        public Result(string message)
        {
            Message = message;
        }
        public bool Success { get; private init; }
        public string? Message { get; }
        public User? User { get; }
    }
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.Email).NotEmpty().EmailAddress();
            RuleFor(m => m.Password).NotEmpty().MaximumLength(50);
        }
    }
    
    public class Handler : IRequestHandler<Command, Result>
    {
        private readonly IAuthRepository _auth;

        public Handler(IAuthRepository auth)
        {
            _auth = auth;
        }
    
        public async Task<Result> Handle(Command command, CancellationToken token)
        {;
            var email = command.Email;
            var password = command.Password;

            var user = await _auth.GetUserByEmail(email);
            if (user == null)
            {
                return new Result("User not found");
            }
            if (!user.CheckPassword(password))
            {
                return new Result("Invalid password");
            }
            return new Result(user);
        }
    }
}
