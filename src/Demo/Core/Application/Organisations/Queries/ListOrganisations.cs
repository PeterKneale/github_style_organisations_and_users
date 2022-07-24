using System.Data;
using Dapper;

namespace Demo.Core.Application.Organisations.Queries;

public static class ListOrganisations
{
    public record Query : IRequest<IEnumerable<Result>>;
    
    public record Result(Guid Id, string Title, string? Description);

    private class Handler : IRequestHandler<Query, IEnumerable<Result>>
    {
        private readonly IDbConnection _db;

        public Handler(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Result>> Handle(Query message, CancellationToken token)
        {
            var sql = "select id, title, description from organisations";
            return await _db.QueryAsync<Result>(sql);
        }
    }
}