using System.Data;
using Dapper;

namespace Demo.Core.Application.Users.Queries;

public static class ListUsers
{
    public record Query : IRequest<IEnumerable<Result>>;

    public record Result(Guid Id, string Name, string Email);
    
    private class Handler : IRequestHandler<Query, IEnumerable<Result>>
    {
        private readonly IDbConnection _db;

        public Handler(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Result>> Handle(Query message, CancellationToken token)
        {
            var sql = "select id, CONCAT(first_name, ' ', last_name) AS name, email from users";
            return await _db.QueryAsync<Result>(sql);
        }
    }
}