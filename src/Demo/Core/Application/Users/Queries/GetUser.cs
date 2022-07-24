using System.Data;
using Dapper;
using FluentValidation;

namespace Demo.Core.Application.Users.Queries;

public static class GetUser
{
    public record Query(Guid UserId) : IRequest<Result>;

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(m => m.UserId).NotEmpty();
        }
    }

    public record Result(Guid Id, string FirstName, string LastName, string Name, string Email);

    private class Handler : IRequestHandler<Query, Result>
    {
        private readonly IDbConnection _db;

        public Handler(IDbConnection db)
        {
            _db = db;
        }

        public async Task<Result> Handle(Query query, CancellationToken token)
        {
            var id = query.UserId;
            var sql = "select id, " +
                      "first_name as firstname, " +
                      "last_name as lastname, " +
                      "CONCAT(first_name, ' ', last_name) AS name, " +
                      "email " +
                      "from users " +
                      "where id = @id";
            var result = await _db.QuerySingleOrDefaultAsync<Result>(sql, new {id});
            if (result == null)
            {
                // todo: not found
                throw new NotImplementedException();
            }
            return result;
        }
    }
}