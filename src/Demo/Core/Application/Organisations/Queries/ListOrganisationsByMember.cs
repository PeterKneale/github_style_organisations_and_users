using System.Data;
using Dapper;

namespace Demo.Core.Application.Organisations.Queries;

public static class ListOrganisationsByMember
{
    public record Query(Guid MemberId) : IRequest<IEnumerable<Result>>;

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
            var memberId = message.MemberId;
            var sql = "select o.id, o.title, o.description " +
                      "from organisation_member m join organisations o on m.organisation_id = o.id " +
                      "where m.member_id = @memberId";
            return await _db.QueryAsync<Result>(sql, new {memberId});
        }
    }
}